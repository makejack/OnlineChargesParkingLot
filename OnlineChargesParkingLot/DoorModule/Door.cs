using System;
using System.Drawing;
using Camera;

namespace OnlineChargesParkingLot.DoorModule
{
    public abstract class Door
    {
        private Action CompleteCallback;

        private string m_LicensePlateNumber { get; set; }
        private DateTime m_Time { get; set; }

        public Door(Action callback)
        {
            this.CompleteCallback = callback;
        }

        public virtual bool Compared(string licensePlatenumber, DateTime time)
        {
            if (string.IsNullOrEmpty(m_LicensePlateNumber) == false)
            {
                if (m_LicensePlateNumber == licensePlatenumber)
                {
                    TimeSpan timeSpan = time - m_Time;
                    if (timeSpan.TotalSeconds < 30)
                    {
                        m_Time = time;
                        return true;
                    }
                }
            }
            m_LicensePlateNumber = licensePlatenumber;
            m_Time = time;
            return false;
        }

        public abstract void Execute(string licensePlateNumber, LicensePlateTypes licensePlateType, Color licensePlateColor, DateTime identificationTime, Image pImage, Image vImage);
    }
}