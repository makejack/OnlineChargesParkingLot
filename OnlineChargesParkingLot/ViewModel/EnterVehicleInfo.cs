using Camera;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OnlineChargesParkingLot.ViewModel
{
    public class EnterVehicleInfo : IdentificationInfo
    {
        public EnterVehicleInfo(string userName, string lNumber, LicensePlateTypes lType, Color lColor, DateTime iTime, Image pImage, Image vImage)
            : base(lNumber, lType, lColor, pImage, vImage, iTime)
        {
            this.UserName = UserName;
            this.UserType = UserType;
        }

        public string UserName { get; set; }

        public string UserType { get; set; }
    }
}
