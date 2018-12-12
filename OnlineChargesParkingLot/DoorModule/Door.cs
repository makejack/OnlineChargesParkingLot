using System;
using System.Drawing;
using Model;
using Camera;
using OnlineChargesParkingLot.OpenModule;

namespace OnlineChargesParkingLot.DoorModule
{
    public abstract class Door
    {
        internal Action CompleteCallback { get; set; }

        internal ParkingLotInfo ParkingLot { get; set; }

        internal OpenOperating OpenOperating { get; set; }

        private string m_LicensePlateNumber;

        private DateTime m_Time;

        public Door(ParkingLotInfo parkingLotInfo, Action callback)
        {
            this.CompleteCallback = callback;
            this.ParkingLot = parkingLotInfo;
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


    }
}