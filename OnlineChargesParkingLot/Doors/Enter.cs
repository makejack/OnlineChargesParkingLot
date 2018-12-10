using System;
using OnlineChargesParkingLot.Interface;

namespace OnlineChargesParkingLot.Doors
{
    public class Enter : IDoor
    {
        private Action m_CallBack;
        public Enter(Action callback)
        {
            m_CallBack = callback;
        }


        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}