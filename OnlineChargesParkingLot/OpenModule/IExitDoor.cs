using System;
using Model;
using OnlineChargesParkingLot.ViewModel;

namespace OnlineChargesParkingLot.OpenModule
{
    public interface IExitDoor
    {
        void Execute(IdentificationInfo iInfo, EnteranceRecord eRecord, OwnerInfo oInfo);
    }
}