using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using OnlineChargesParkingLot.ViewModel;

namespace OnlineChargesParkingLot.OpenModule
{
    /// <summary>
    /// 识别即开门
    /// </summary>
    public class EnterIdentificationOpening : OpenOperating
    {
        public override void Execute(OwnerInfo ownerInfo)
        {
            EnterIdentificationInfo info = new EnterIdentificationInfo();
            if (ownerInfo != null)
            {
                if (ownerInfo.UserType == 1) //黑名单
                {
                    //不开门
                }
                //开门
            }
            else
            {
                
            }
            
        }
    }
}
