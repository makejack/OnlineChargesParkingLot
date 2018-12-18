using System;
using Model;
using OnlineChargesParkingLot.ViewModel;

namespace OnlineChargesParkingLot.OpenModule
{
    public class ExitFixedUserOpenTheDoor : BaseOperating, IExitDoor
    {
        private Action PlateInfoCallBack { get; set; }

        public ExitFixedUserOpenTheDoor(Action callback)
        {
            this.PlateInfoCallBack = callback;
        }

        public void Execute(IdentificationInfo iInfo, EnteranceRecord eRecord, OwnerInfo oInfo)
        {
            string userType = "临时车辆";
            int day = 255;
            bool openTheDoor = true;
            if (oInfo != null)
            {
                if (oInfo.PlateType == 0) //月租车辆
                {
                    userType = "月租车辆";
                    day = SurplusDays(oInfo.StopTime);
                    if (day == 0)
                    {
                        //过期
                        userType += "（过期）";

                        openTheDoor = false;
                    }
                    //开门
                }
                else if (oInfo.PlateType == 1)//固定车辆
                {
                    userType = "固定车辆";
                }
                else if (oInfo.PlateType == 2) //定距卡车辆
                {
                    userType = "定距卡车辆";
                }

                if (oInfo.UserType == 1) //黑名单
                {
                    //不开门
                    userType += "（黑名单）";

                    openTheDoor = false;
                }
            }
            else
            {
                openTheDoor = false;
            }

            OpenTheDoor(iInfo.LicensePlateNumber, iInfo.IdentificationTime, openTheDoor);
            
            PlateInfoCallBack();
        }
    }
}