using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Camera;
using Model;
using OnlineChargesParkingLot.ViewModel;

namespace OnlineChargesParkingLot.OpenModule
{
    public interface IEnterDoor
    {

        void Execute(IdentificationInfo iInfo, OwnerInfo ownerInfo);
    }
}
