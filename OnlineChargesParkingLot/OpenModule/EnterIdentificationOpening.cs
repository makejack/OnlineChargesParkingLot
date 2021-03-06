﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Camera;
using Model;
using OnlineChargesParkingLot.ViewModel;

namespace OnlineChargesParkingLot.OpenModule
{
    /// <summary>
    /// 识别即开门
    /// </summary>
    public class EnterIdentificationOpening : BaseOperating, IEnterDoor
    {
        private Action<EnterVehicleInfo> PlateInfoCallBack { get; set; }

        public EnterIdentificationOpening(Action<EnterVehicleInfo> callback)
        {
            this.PlateInfoCallBack = callback;
        }

        public void Execute(IdentificationInfo iInfo, OwnerInfo ownerInfo)
        {
            string userName = string.Empty;
            string userType = "临时车辆";
            //string vehicleType = VehicleTypeToStr(iInfo.LicensePlateType);
            int day = 255;
            bool openTheDoor = true;
            if (ownerInfo != null)
            {
                userName = ownerInfo.UserName;
                if (ownerInfo.PlateType == 0)
                {
                    userType = "月租车辆";
                    day = SurplusDays(ownerInfo.StopTime);
                    if (day == 0)
                    {
                        userType += "（过期）";
                    }
                }
                else if (ownerInfo.PlateType == 1)
                {
                    userType = "固定车辆";
                }
                else if (ownerInfo.PlateType == 2)
                {
                    userType = "定距卡车辆";
                }

                if (ownerInfo.UserType == 1) //黑名单
                {
                    //不开门
                    userType += "（黑名单）";

                    openTheDoor = false;
                }
            }

            //开门
            OpenTheDoor(iInfo.LicensePlateNumber, iInfo.IdentificationTime, openTheDoor);

            EnterVehicleInfo enterInfo = (EnterVehicleInfo)iInfo;
            enterInfo.UserName = userName;
            enterInfo.UserType = userType;

            PlateInfoCallBack(enterInfo);
        }
    }
}
