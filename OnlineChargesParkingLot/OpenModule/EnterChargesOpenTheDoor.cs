using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace OnlineChargesParkingLot.OpenModule
{
    public class EnterChargesOpenTheDoor : OpenOperating
    {
        public override void Execute(OwnerInfo ownerInfo)
        {
            if (ownerInfo != null)
            {
                if (ownerInfo.PlateType == 0) //月租车辆
                {
                    if (ownerInfo.UserType == 1) //月租车辆（黑名单）
                    {
                        //不开门
                    }

                    int day = SurplusDays(ownerInfo.StopTime);
                    if (day == 0)
                    {
                        //过期
                    }
                    //开门
                }
                else if (ownerInfo.PlateType == 1)//固定车辆
                {
                    if (ownerInfo.UserType == 1) //固定车辆（黑名单）
                    {
                        //不开门
                    }
                    //开门
                }
                else if (ownerInfo.PlateType == 2) //定距卡车辆
                {
                    //不开门
                }
            }
            else
            {
                //临时车辆
            }
        }
    }
}
