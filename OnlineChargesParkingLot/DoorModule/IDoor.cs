using Camera;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OnlineChargesParkingLot.ViewModel;

namespace OnlineChargesParkingLot.DoorModule
{
    public interface IDoor
    {
        void Execute(IdentificationInfo info);
    }
}
