using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Camera;
using Model;
using OnlineChargesParkingLot.ViewModel;

namespace OnlineChargesParkingLot.OpenModule
{
    public class EnterFixedUserOpenTheDoor : BaseOperating, IEnterDoor
    {
        private Action<EnterVehicleInfo> PlateInfoCallBack { get; set; }

        public EnterFixedUserOpenTheDoor(Action<EnterVehicleInfo> callback)
        {
            this.PlateInfoCallBack = callback;
        }

        public void Execute(IdentificationInfo iInfo, OwnerInfo ownerInfo)
        {
            string userName = string.Empty;
            string userType = "临时车辆";
            string vehicleType = VehicleTypeToStr(iInfo.LicensePlateType);
            int day = 255;
            try
            {
                if (ownerInfo != null)
                {
                    if (ownerInfo.PlateType == 0) //月租车辆
                    {
                        userType = "月租车辆";
                        day = SurplusDays(ownerInfo.StopTime);
                        if (day == 0)
                        {
                            //过期
                            userType += "（过期）";
                        }
                        //开门
                    }
                    else if (ownerInfo.PlateType == 1)//固定车辆
                    {
                        userType = "固定车辆";
                    }
                    else if (ownerInfo.PlateType == 2) //定距卡车辆
                    {
                        userType = "定距卡车辆";
                    }

                    if (ownerInfo.UserType == 1) //黑名单
                    {
                        //不开门
                        userType += "（黑名单）";
                    }
                }
                //不开门
            }
            finally
            {
                EnterVehicleInfo enterInfo = (EnterVehicleInfo)iInfo;
                enterInfo.UserName = userName;
                enterInfo.UserType = userType;

                PlateInfoCallBack(enterInfo);
            }
        }
    }
}
