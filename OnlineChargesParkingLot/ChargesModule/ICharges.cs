using System;

namespace OnlineChargesParkingLot.ChargesModule
{
    public interface ICharges
    {
        double Calculation(DateTime enterTime, DateTime exitTime);
    }
}