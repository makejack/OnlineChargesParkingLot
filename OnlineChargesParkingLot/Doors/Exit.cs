using System;
using OnlineChargesParkingLot.Interface;

namespace OnlineChargesParkingLot.Doors
{
    public class Exit : IDoor
    {
        private Action m_CallBack;
        public Exit(Action callback)
        {
            m_CallBack = callback;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}