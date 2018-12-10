using System;
using System.Drawing;
using Camera;

namespace OnlineChargesParkingLot.Model
{
    public class EnterModel
    {
        public EnterModel(string licensePlateNumber, DateTime identificationTime, LicensePlateTypes licensePlateType, Color licensePlateColor, Image pImage, Image vImage)
        {
            this.LicensePlateNumber = licensePlateNumber;
            this.IdentificationTime = identificationTime;
            this.LicensePlateType = licensePlateType;
            this.LicensePlateColor = licensePlateColor;
            this.PanoramaImage = pImage;
            this.VehicleImage = vImage;
        }

        /// <summary>
        /// 车牌号码
        /// </summary>
        public string LicensePlateNumber { get; set; }

        /// <summary>
        /// 识别时间
        /// </summary>
        public DateTime IdentificationTime { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public LicensePlateTypes LicensePlateType { get; }

        /// <summary>
        /// 车牌颜色
        /// </summary>
        public Color LicensePlateColor { get; }

        /// <summary>
        /// 完整的图像
        /// </summary>
        public Image PanoramaImage { get; }

        /// <summary>
        /// 车牌的图像
        /// </summary>
        public Image VehicleImage { get; }
        
    }
}