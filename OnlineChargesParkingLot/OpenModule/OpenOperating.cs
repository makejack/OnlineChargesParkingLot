using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace OnlineChargesParkingLot.OpenModule
{
    public abstract class OpenOperating
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

        public abstract void Execute(OwnerInfo ownerInfo);
    }
}
