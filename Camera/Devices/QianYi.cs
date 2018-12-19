using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Camera.SDK;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;
using LogHelper;
using System.Threading.Tasks;

namespace Camera.Devices
{
    internal class QianYi : Device
    {

        private QianYiSDK.FNetFindDeviceCallback CameraFindCameraCallBack { get; set; }

        private QianYiSDK.FGetImageCB CameraPlateReceivedCallBack { get; set; }


        public QianYi()
        {

            CameraPlateReceivedCallBack = PlateReceived;

            int ret = QianYiSDK.Net_Init();
            if (ret == 0)
            {
                QianYiSDK.Net_RegImageRecv(CameraPlateReceivedCallBack);
            }
        }

        public override void UnInit()
        {
            QianYiSDK.Net_UNinit();
        }

        public override bool Close(ConnectionConfiguration configuration)
        {
            QianYiSDK.Net_StopVideo(configuration.CameraHwnd);
            QianYiSDK.Net_DisConnCamera(configuration.CameraHwnd);
            QianYiSDK.Net_DelCamera(configuration.CameraHwnd);
            return true;
        }

        public override bool Connection(ConnectionConfiguration configuration)
        {
            int cameraHwnd = QianYiSDK.Net_AddCamera(configuration.IP);
            if (cameraHwnd == 0)
            {
                int iRet = QianYiSDK.Net_ConnCamera(cameraHwnd, configuration.Port, 5);
                if (iRet == 0)
                {
                    iRet = QianYiSDK.Net_StartVideo(cameraHwnd, configuration.StreamType, configuration.ContainerHwnd);
                    if (iRet == 0)
                    {
                        configuration.CameraHwnd = cameraHwnd;
                        return true;
                    }
                    else
                    {
                        QianYiSDK.Net_DisConnCamera(cameraHwnd);
                        QianYiSDK.Net_DelCamera(cameraHwnd);
                    }
                }
                else
                {
                    QianYiSDK.Net_DelCamera(cameraHwnd);
                }
            }
            return false;
        }

        public override void FindCamera()
        {
            if (CameraFindCameraCallBack == null)
            {
                CameraFindCameraCallBack = FindCamera;
                Task.Factory.StartNew(() =>
                {
                    QianYiSDK.Net_FindDevice(CameraFindCameraCallBack, IntPtr.Zero);
                });
            }
        }

        private int FindCamera(ref QianYiSDK.T_RcvMsg ptFindDevice, IntPtr obj)
        {
            string ip = QianYiSDK.IntToIp(QianYiSDK.Reverse_uint(ptFindDevice.tNetSetup.uiIPAddress));
            FindCameraCallback(ip, 30000, this.GetType().Name);

            return 0;
        }

        private int PlateReceived(int tHandle, uint uiImageId, ref QianYiSDK.T_ImageUserInfo tImageInfo, ref QianYiSDK.T_PicInfo tPicInfo)
        {
            //车辆图像
            if (tImageInfo.ucViolateCode != 0) return 0;
            if (tImageInfo.szLprResult == null) return 0;

            try
            {
                //车牌号码
                string strLicensePlateNumber = Encoding.Default.GetString(tImageInfo.szLprResult).Replace("\0", "");
                if (string.IsNullOrEmpty(strLicensePlateNumber)) return 0;
                //车牌类型
                LicensePlateTypes licensePlateType = (LicensePlateTypes)tImageInfo.ucLprType;
                //车牌颜色
                Color licensePlateColor = Color.Blue;

                switch (tImageInfo.ucPlateColor)
                {
                    case 0://蓝色
                        break;
                    case 1://黄色
                        licensePlateColor = Color.Yellow;
                        break;
                    case 2://白色
                        licensePlateColor = Color.White;
                        break;
                    case 3://黑色
                        licensePlateColor = Color.Black;
                        break;
                    case 4://其它颜色
                        licensePlateColor = Color.Green;
                        break;
                    case 5://枚举类型数量，不作为输入参数
                        break;
                }

                string pImagePath, vImagePath;
                DateTime now = SetImagePath(strLicensePlateNumber, out pImagePath, out vImagePath);

                //全景图片
                Image panoramaImage = GetCameraImage(tPicInfo.ptPanoramaPicBuff, (int)tPicInfo.uiPanoramaPicLen, pImagePath);
                //车牌图片
                Image vehicleImage = GetCameraImage(tPicInfo.ptVehiclePicBuff, (int)tPicInfo.uiVehiclePicLen, vImagePath);

                PlateReceviedCallback(tHandle, string.Empty, strLicensePlateNumber, licensePlateType, licensePlateColor, panoramaImage, vehicleImage, now);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
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
                Log.Error("无法获取识别图像：" + ex.Message, ex);
            }
            return null;
        }

        public override bool ManualCapture(ConnectionConfiguration configuration, string strFullPath)
        {
            int iRet = QianYiSDK.Net_SaveJpgFile(configuration.CameraHwnd, strFullPath);
            if (iRet == 0)
            {
                return true;
            }
            return false;
        }

    }
}
