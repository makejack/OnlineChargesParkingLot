using System;
using System.Drawing;
using Camera;

namespace OnlineChargesParkingLot.DoorModule
{
    public class Exit : Door
    {
        public Exit(Action callback) : base(callback)
        {

        }

        public override void Execute(string licensePlateNumber, LicensePlateTypes licensePlateType, Color licensePlateColor, DateTime identificationTime, Image pImage, Image vImage)
        {
            bool ret = Compared(licensePlateNumber, identificationTime);
            if (ret)
            {
                return;
            }
        }
    }
}