using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace OnlineChargesParkingLot.OpenModule
{
    public class EnterFixedUserOpenTheDoor : OpenOperating
    {
        public override void Execute(OwnerInfo ownerInfo)
        {
            string userType = "临时车辆";
            if (ownerInfo != null)
            {
                userType = ownerInfo.PlateType == 0 ? "月租车辆" : "固定车辆";

                if (ownerInfo.UserType == 1) //黑名单
                {
                    //不开门
                    userType += "（黑名单）";
                }
                else if (ownerInfo.PlateType == 0) //月租车辆
                {
                    int day = SurplusDays(ownerInfo.StopTime);
                    if (day == 0)
                    {
                        //过期
                        userType += "过期";
                    }
                }

                //开门
            }

        }
    }
}
