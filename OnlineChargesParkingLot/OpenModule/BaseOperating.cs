using Camera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineChargesParkingLot.OpenModule
{
    public abstract class BaseOperating
    {

        public virtual int SurplusDays(DateTime date)
        {
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = date.Date - now.Date;
            double days = timeSpan.TotalDays;
            if (days < 0)
            {
                days = 0;
            }
            else if (days >= 8)
            {
                days = 255;
            }
            return (int)days;
        }

        public virtual string VehicleTypeToStr(LicensePlateTypes licensePlateType)
        {
            string strType = "未知";
            switch (licensePlateType)
            {
                case LicensePlateTypes.LT_UNKNOWN:
                    break;
                case LicensePlateTypes.LT_BLUE:
                    strType = "蓝牌小汽车";
                    break;
                case LicensePlateTypes.LT_BLACK:
                    strType = "黑牌小汽车";
                    break;
                case LicensePlateTypes.LT_YELLOW:
                    strType = "单排黄牌";
                    break;
                case LicensePlateTypes.LT_YELLOW2:
                    strType = "双排黄牌";
                    break;
                case LicensePlateTypes.LT_POLICE:
                    strType = "警车车牌";
                    break;
                case LicensePlateTypes.LT_ARMPOL:
                    strType = "武警车牌";
                    break;
                case LicensePlateTypes.LT_ARMPOL2:
                    strType = "双排武警车牌";
                    break;
                case LicensePlateTypes.LT_INDIVI:
                    strType = "个性化车牌";
                    break;
                case LicensePlateTypes.LT_ARMY:
                    strType = "单排军车牌";
                    break;
                case LicensePlateTypes.LT_ARMY2:
                    strType = "双排军车牌";
                    break;
                case LicensePlateTypes.LT_EMBASSY:
                    strType = "使馆车牌";
                    break;
                case LicensePlateTypes.LT_HONGKONG:
                    strType = "香港进出中国车牌";
                    break;
                case LicensePlateTypes.LT_TRACTOR:
                    strType = "农用车牌";
                    break;
                case LicensePlateTypes.LT_COACH:
                    strType = "教练车牌";
                    break;
                case LicensePlateTypes.LT_MACAO:
                    strType = "澳门进出中国车牌";
                    break;
            }
            return strType;
        }

    }
}
