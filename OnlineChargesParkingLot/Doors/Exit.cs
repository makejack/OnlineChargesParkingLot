using System;
using System.Drawing;
using Camera;
using OnlineChargesParkingLot.Interface;
using OnlineChargesParkingLot.Model;

namespace OnlineChargesParkingLot.Doors
{
    public class Exit : IDoor
    {
        private Action m_CallBack;

        public Exit(Action callback)
        {
            m_CallBack = callback;
        }

        private ExitModel CurrentVehicle { get; set; }

        public void Execute(string licensePlateNumber, LicensePlateTypes licensePlateType, Color licensePlateColor, DateTime identificationTime, Image pImage, Image vImage)
        {
            if (CurrentVehicle != null)
            {
                if (CurrentVehicle.LicensePlateNumber == licensePlateNumber)
                {
                    TimeSpan timeSpan = identificationTime - CurrentVehicle.IdentificationTime;
                    if (timeSpan.TotalSeconds < 30)
                    {
                        CurrentVehicle.IdentificationTime = identificationTime;
                        return;
                    }
                }
            }
            
        }
    }
}