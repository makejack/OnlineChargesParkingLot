using System;
using System.Drawing;
using Camera;

namespace OnlineChargesParkingLot.Interface
{
    public interface IDoor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="licensePlateNumber">车牌号码</param>
        /// <param name="licensePlateType">车牌类型</param>
        /// <param name="licensePlateColor">车牌颜色</param>
        /// <param name="identificationTime">识别时间</param>
        /// <param name="pImage">全景图片</param>
        /// <param name="vImage">车牌图片</param>
        void Execute(string licensePlateNumber, LicensePlateTypes licensePlateType, Color licensePlateColor, DateTime identificationTime, Image pImage, Image vImage);
    }
}