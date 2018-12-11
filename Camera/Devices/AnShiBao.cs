using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Camera.SDK;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using LogHelper;

namespace Camera.Devices
{
    /// <summary>
    /// 安视宝摄像机
    /// </summary>
    internal class AnShiBao : Device
    {

        private AnShiBaoSDK.IPCSDK_CALLBACK CameraPlateReceivedCallBack { get; set; }

        private Task FindTask { get; set; }

        public AnShiBao()
        {
            CameraPlateReceivedCallBack = PlateReceived;

            int ret = AnShiBaoSDK.IPCSDK_Init(8910);
            if (ret == 0)
            {
                AnShiBaoSDK.IPCSDK_Register_Callback(CameraPlateReceivedCallBack);
            }
        }

        public event FindCameraHandle FindCameraChange;
        public event PlateReceivedHandle PlateReceivedChange;

        public override void UnInit()
        {
            AnShiBaoSDK.IPCSDK_UnInit();
        }

        public override bool Close(ConnectionConfiguration configuration)
        {
            int iRet = AnShiBaoSDK.IPCSDK_Stop_Stream(configuration.IP);
            return iRet == 0;
        }

        public override bool Connection(ConnectionConfiguration configuration)
        {
            int cameraHwnd = AnShiBaoSDK.IPCSDK_Start_Stream(configuration.MainHwnd, configuration.ContainerHwnd, configuration.IP, configuration.StreamType);
            if (cameraHwnd == 0)
            {
                configuration.CameraHwnd = cameraHwnd;
                return true;
            }
            return false;
        }

        public override void FindCamera()
        {
            if (FindTask == null)
            {
                FindTask = Task.Factory.StartNew(() =>
                {
                    int cameraNum = 0;
                    int ipSize = Marshal.SizeOf(typeof(AnShiBaoSDK.CAMERA_IP_TAG));
                    IntPtr pCameraList = Marshal.AllocHGlobal(ipSize * 128);
                    try
                    {
                        int iRet = AnShiBaoSDK.IPCSDK_Find_Camera(ref cameraNum, pCameraList);
                        if (iRet != 0) return;
                        if (cameraNum <= 0) return;

                        for (int i = 0; i < cameraNum; i++)
                        {
                            int hwnd = (int)pCameraList + i * ipSize;
                            AnShiBaoSDK.CAMERA_IP_TAG cameraParam = (AnShiBaoSDK.CAMERA_IP_TAG)Marshal.PtrToStructure((IntPtr)hwnd, typeof(AnShiBaoSDK.CAMERA_IP_TAG));

                            //方法回调
                            FindCameraCallback(cameraParam.ip, cameraParam.port, this.GetType().Name);
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message, ex);
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(pCameraList);
                    }

                    FindTask = null;
                });
            }
        }

        private int PlateReceived(string ip, IntPtr buff, int len)
        {
            /* 16KB 用于存储车牌信息足够了 */
            IntPtr pPlateResult = IntPtr.Zero;
            // 车牌特写图临时空间
            IntPtr pPlateJpeg = IntPtr.Zero;
            //获取车牌号
            IntPtr pLicensePlate = IntPtr.Zero;
            //获取车牌颜色
            IntPtr pLinceseColor = IntPtr.Zero;
            try
            {
                pPlateResult = Marshal.AllocHGlobal(16 * 1024);
                pPlateJpeg = Marshal.AllocHGlobal(32 * 1024);
                int lenght = 0;
                int iRet = AnShiBaoSDK.IPCSDK_Get_Plate_Info(buff, pPlateResult, ref lenght);
                if (iRet == 0)
                {
                    pLicensePlate = Marshal.AllocHGlobal(20);
                    iRet = AnShiBaoSDK.IPCSDK_Get_Plate_License(pPlateResult, pLicensePlate);
                    if (iRet == 0)
                    {
                        //车牌号码
                        string strLicensePlateNumber = Marshal.PtrToStringAnsi(pLicensePlate);
                        //车牌类型
                        LicensePlateTypes licensePlateType = LicensePlateTypes.LT_BLUE;
                        //车牌颜色
                        Color licensePlateColor = Color.Blue;

                        pLinceseColor = Marshal.AllocHGlobal(8);
                        iRet = AnShiBaoSDK.IPCSDK_Get_Plate_Color(pPlateResult, pLinceseColor);
                        if (iRet == 0)
                        {
                            string strPlateColor = Marshal.PtrToStringAnsi(pLinceseColor);
                            switch (strPlateColor)
                            {
                                case "黄":
                                    licensePlateColor = Color.Yellow;
                                    licensePlateType = LicensePlateTypes.LT_YELLOW;
                                    break;
                                case "白":
                                    licensePlateColor = Color.White;
                                    break;
                                case "黑":
                                    licensePlateColor = Color.Black;
                                    licensePlateType = LicensePlateTypes.LT_BLACK;
                                    break;
                                case "绿":
                                    licensePlateColor = Color.Green;
                                    break;
                            }
                        }

                        string pImagePath, vImagePath;
                        DateTime now = SetImagePath(strLicensePlateNumber, out pImagePath, out vImagePath);

                        //全景图片
                        Image panoramaImage = GetCameraImage(buff, len, pImagePath);
                        //车牌图片
                        Image vehicleImage = null;

                        iRet = AnShiBaoSDK.IPCSDK_Get_Plate_Jpeg(buff, pPlateJpeg, ref lenght);
                        if (iRet == 0)
                        {
                            //获取车牌图片
                            vehicleImage = GetCameraImage(pPlateJpeg, lenght, vImagePath);
                        }

                        PlateReceviedCallback(-1, ip, strLicensePlateNumber, licensePlateType, licensePlateColor, panoramaImage, vehicleImage, now);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
            finally
            {
                if (pLinceseColor != IntPtr.Zero)
                    Marshal.FreeHGlobal(pLinceseColor);
                if (pPlateResult != IntPtr.Zero)
                    Marshal.FreeHGlobal(pPlateResult);
                if (pPlateJpeg != IntPtr.Zero)
                    Marshal.FreeHGlobal(pPlateJpeg);
                if (pLicensePlate != IntPtr.Zero)
                    Marshal.FreeHGlobal(pLicensePlate);
            }
            return 0;
        }

        private Image GetCameraImage(IntPtr hwnd, int len, string fileName)
        {
            byte[] buttfer = new byte[len];
            try
            {
                Marshal.Copy(hwnd, buttfer, 0, len);
                using (Stream stream = new MemoryStream(buttfer))
                {
                    Image img = new Bitmap(stream);
                    img.Save(fileName);
                    return img;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
            return null;
        }

        public override bool ManualCapture(ConnectionConfiguration configuration, string strFullPath)
        {
            int iRet = AnShiBaoSDK.IPCSDK_Manual_Capture_Write_File(configuration.IP, strFullPath);
            if (iRet == 0)
            {
                return true;
            }
            return false;
        }
    }
}
