using Camera;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace OnlineChargesParkingLot.DoorModule
{
    public interface IDoor
    {
        void Execute(string licensePlateNumber, LicensePlateTypes licensePlateType, Color licensePlateColor, DateTime identificationTime);
    }
}
