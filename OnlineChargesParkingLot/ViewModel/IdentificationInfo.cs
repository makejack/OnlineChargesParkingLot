using Camera;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OnlineChargesParkingLot.ViewModel
{
    public class IdentificationInfo
    {
        public IdentificationInfo(string lNumber, LicensePlateTypes lType, Color lColor, Image pImage, Image vImage, DateTime iTime)
        {
            this.LicensePlateNumber = lNumber;
            this.LicensePlateType = lType;
            this.LicensePlateColor = lColor;
            this.PanoramaImage = pImage;
            this.VehicleImage = vImage;
            this.IdentificationTime = iTime;
        }

        /// <summary>
        /// 车牌号码
        /// </summary>
        public string LicensePlateNumber { get; }

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

        /// <summary>
        /// 识别的时间
        /// </summary>
        public DateTime IdentificationTime { get; }
    }
}
