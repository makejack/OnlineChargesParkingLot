using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Camera.Interface;
using Camera.SDK;
using Camera.Model;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;
using LogHelper;

namespace Camera.Device
{
    internal class QianYi : ICamera
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

        public void UnInit()
        {
            QianYiSDK.Net_UNinit();
        }

        public void Close(ConnectionConfiguration configuration)
        {
            QianYiSDK.Net_StopVideo(configuration.CameraHwnd);
            QianYiSDK.Net_DisConnCamera(configuration.CameraHwnd);
            QianYiSDK.Net_DelCamera(configuration.CameraHwnd);
            Common.Default.Del(configuration);
        }

        public bool Connection(ConnectionConfiguration configuration)
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

                        Common.Default.Add(configuration);

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

        public void FindCamera()
        {
            if (CameraFindCameraCallBack == null)
            {
                CameraFindCameraCallBack = FindCamera;
                QianYiSDK.Net_FindDevice(CameraFindCameraCallBack, IntPtr.Zero);
            }
        }

        private int FindCamera(ref QianYiSDK.T_RcvMsg ptFindDevice, IntPtr obj)
        {
            CameraEventArgs camera = new CameraEventArgs(QianYiSDK.IntToIp(QianYiSDK.Reverse_uint(ptFindDevice.tNetSetup.uiIPAddress)), 30000, this.GetType().Name);
            Common.Default.ExecuteFindCamera(this, camera);

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

                DateTime date = DateTime.Now;
                string dateDirectory = Common.Default.ImagePath + @"\" + date.ToString("yyyyMMdd");
                if (!Directory.Exists(dateDirectory))
                {
                    Directory.CreateDirectory(dateDirectory);
                }
                string dateTimeDirectory = date.ToString("yyyyMMddHHmmss");
                string panoramaPath = dateDirectory + $@"\{dateTimeDirectory}_{strLicensePlateNumber}.jpg";
                string vehiclePath = dateDirectory + $@"\{dateTimeDirectory}_{strLicensePlateNumber}_plate.jpg";

                //全景图片
                Image panoramaImage = GetCameraImage(tPicInfo.ptPanoramaPicBuff, (int)tPicInfo.uiPanoramaPicLen, panoramaPath);
                //车牌图片
                Image vehicleImage = GetCameraImage(tPicInfo.ptVehiclePicBuff, (int)tPicInfo.uiVehiclePicLen, vehiclePath);

                string ip = Common.Default.CameraHandleToIp(tHandle);
                PlateEventArgs info = new PlateEventArgs(tHandle, ip, strLicensePlateNumber, licensePlateType, licensePlateColor, panoramaImage, vehicleImage, date);

                Common.Default.ExecutePlateReceived(this, info);
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

        public bool ManualCapture(ConnectionConfiguration configuration, string strFullPath)
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
