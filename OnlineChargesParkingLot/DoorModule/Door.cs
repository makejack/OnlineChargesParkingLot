using System;
using System.Drawing;
using Model;
using Camera;
using OnlineChargesParkingLot.OpenModule;

namespace OnlineChargesParkingLot.DoorModule
{
    public abstract class Door
    {
        private string m_LicensePlateNumber;

        private DateTime m_Time;
        
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


    }
}