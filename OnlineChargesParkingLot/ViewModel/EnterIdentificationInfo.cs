using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineChargesParkingLot.ViewModel
{
    public class EnterIdentificationInfo
    {
        public string UserName { get; set; }

        public string LicensePlateNumber { get; set; }

        public DateTime EnterTime { get; set; }

        public string VehicleType { get; set; }

        public string UserType { get; set; }
    }
}
