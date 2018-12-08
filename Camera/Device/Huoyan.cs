using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Camera.Interface;
using Camera.SDK;
using Camera.Model;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using LogHelper;

namespace Camera.Device
{
    internal class Huoyan : ICamera
    {

        private HuoYanSDK.VZLPRC_FIND_DEVICE_CALLBACK CameraFindCameraCallBack { get; set; }

        private HuoYanSDK.VZLPRC_PLATE_INFO_CALLBACK CameraPlateReceivedCallBack { get; set; }

        public Huoyan()
        {
            CameraPlateReceivedCallBack = PlateReceived;

            HuoYanSDK.VzLPRClient_Setup();
        }

        public void UnInit()
        {
            HuoYanSDK.VzLPRClient_Cleanup();
        }

        public void Close(ConnectionConfiguration configuration)
        {
            if (configuration.CameraHwnd != (int)IntPtr.Zero)
            {
                int iRet = HuoYanSDK.VzLPRClient_StopRealPlay(configuration.PlayHwnd);
                if (iRet == 0)
                {
                    HuoYanSDK.VzLPRClient_SetPlateInfoCallBack(configuration.CameraHwnd, null, IntPtr.Zero, 1);

                    HuoYanSDK.VzLPRClient_Close(configuration.CameraHwnd);
                    Common.Default.Del(configuration);
                }
            }
        }

        public bool Connection(ConnectionConfiguration configuration)
        {
            int m_hLPRClinet = HuoYanSDK.VzLPRClient_Open(configuration.IP, configuration.Port, "admin", "admin");
            if (m_hLPRClinet != 0)
            {
                configuration.CameraHwnd = m_hLPRClinet;

                int m_hPlay = HuoYanSDK.VzLPRClient_StartRealPlay(m_hLPRClinet, configuration.ContainerHwnd);
                if (m_hPlay > -1)
                {
                    HuoYanSDK.VzLPRClient_SetPlateInfoCallBack(m_hLPRClinet, CameraPlateReceivedCallBack, IntPtr.Zero, 1);

                    configuration.PlayHwnd = m_hPlay;
                    Common.Default.Add(configuration);

                    return true;
                }
                else
                {
                    HuoYanSDK.VzLPRClient_Close(m_hLPRClinet);
                }
            }
            return false;
        }

        public void FindCamera()
        {
            if (CameraFindCameraCallBack == null)
            {
                CameraFindCameraCallBack = FindCamera;
                HuoYanSDK.VZLPRClient_StartFindDevice(CameraFindCameraCallBack, IntPtr.Zero);
            }
        }

        private void FindCamera(string pStrDevName, string pStrIPAddr, ushort usPort1, ushort usPort2, uint SL, uint SH, IntPtr pUserData)
        {
            CameraEventArgs camera = new CameraEventArgs(pStrIPAddr, usPort1, this.GetType().Name);
            Common.Default.ExecuteFindCamera(this, camera);
        }

        private int PlateReceived(int handle, IntPtr pUserData, IntPtr pResult, uint uNumPlates, HuoYanSDK.VZ_LPRC_RESULT_TYPE eResultType, IntPtr pImgFull, IntPtr pImgPlateClip)
        {
            try
            {
                //获取车牌识别结果信息
                HuoYanSDK.TH_PlateResult plateResult;
                if (eResultType != HuoYanSDK.VZ_LPRC_RESULT_TYPE.VZ_LPRC_RESULT_REALTIME)
                {
                    plateResult = (HuoYanSDK.TH_PlateResult)Marshal.PtrToStructure(pResult, typeof(HuoYanSDK.TH_PlateResult));
                    //车牌号码
                    string strLicensePlateNumber = new string(plateResult.license).Replace("\0", "");
                    if (string.IsNullOrEmpty(strLicensePlateNumber)) return 0;

                    //车辆类型
                    LicensePlateTypes licensePlateType = (LicensePlateTypes)plateResult.nType;
                    //车牌颜色
                    Color licensePlateColor = Color.Blue;
                    switch (plateResult.nColor)
                    {
                        case 0://未知
                            break;
                        case 1://蓝色
                            break;
                        case 2://黄色
                            licensePlateColor = Color.Yellow;
                            break;
                        case 3://白色
                            licensePlateColor = Color.White;
                            break;
                        case 4://黑色
                            licensePlateColor = Color.Black;
                            break;
                        case 5://绿色
                            licensePlateColor = Color.Green;
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

                    HuoYanSDK.VzLPRClient_ImageSaveToJpeg(pImgFull, panoramaPath, 100);
                    HuoYanSDK.VzLPRClient_ImageSaveToJpeg(pImgPlateClip, vehiclePath, 100);

                    Image panoramaImage = new Bitmap(panoramaPath);
                    Image vehicleImage = new Bitmap(vehiclePath);

                    string ip = Common.Default.CameraHandleToIp(handle);
                    PlateEventArgs info = new PlateEventArgs(handle, ip, strLicensePlateNumber, licensePlateType, licensePlateColor, panoramaImage, vehicleImage);
                    Common.Default.ExecutePlateReceived(this, info);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
            return 0;
        }

        public bool ManualCapture(ConnectionConfiguration configuration, string strFullPath)
        {
            int iRet = HuoYanSDK.VzLPRClient_GetSnapShootToJpeg2(configuration.PlayHwnd, strFullPath, 100);
            if (iRet == 0)
            {
                return true;
            }
            return false;
        }

    }
}
