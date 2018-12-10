using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Camera
{
    public enum LicensePlateTypes
    {
        /// <summary>
        /// 未知
        /// </summary>
        LT_UNKNOWN = 0,
        /// <summary>
        /// 蓝牌小汽车
        /// </summary>
        LT_BLUE = 1,
        /// <summary>
        /// 黑牌小汽车
        /// </summary>
        LT_BLACK = 2,
        /// <summary>
        /// 单排黄牌
        /// </summary>
        LT_YELLOW = 3,
        /// <summary>
        /// 双排黄牌（大车尾牌，农用车）
        /// </summary>
        LT_YELLOW2 = 4,
        /// <summary>
        /// 警车车牌
        /// </summary>
        LT_POLICE = 5,
        /// <summary>
        /// 武警车牌
        /// </summary>
        LT_ARMPOL = 6,
        /// <summary>
        /// 个性化车牌
        /// </summary>
        LT_INDIVI = 7,
        /// <summary>
        /// 单排军车牌
        /// </summary>
        LT_ARMY = 8,
        /// <summary>
        /// 双排军车牌
        /// </summary>
        LT_ARMY2 = 9,
        /// <summary>
        /// 使馆车牌
        /// </summary>
        LT_EMBASSY = 10,
        /// <summary>
        /// 香港进出中国大陆车牌
        /// </summary>
        LT_HONGKONG = 11,
        /// <summary>
        /// 农用车牌
        /// </summary>
        LT_TRACTOR = 12,
        /// <summary>
        /// 教练车牌
        /// </summary>
        LT_COACH = 13,
        /// <summary>
        /// 澳门进出中国大陆车牌
        /// </summary>
        LT_MACAO = 14,
        /// <summary>
        /// 双层武警车牌
        /// </summary>
        LT_ARMPOL2 = 15,
    }

    public class PlateEventArgs : EventArgs
    {
        public PlateEventArgs(int handle, string ip, string licensePlateNumber, LicensePlateTypes licensePlateType, Color licensePlateColor, Image panoramaImage, Image vehicleImage, DateTime identificationTime)
        {
            Handle = handle;
            IP = ip;
            LicensePlateNumber = licensePlateNumber;
            LicensePlateType = licensePlateType;
            LicensePlateColor = licensePlateColor;
            PanoramaImage = panoramaImage;
            VehicleImage = vehicleImage;
            IdentificationTime = identificationTime;
        }


        /// <summary>
        /// 摄像机的句柄
        /// </summary>
        public int Handle { get; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; }

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
