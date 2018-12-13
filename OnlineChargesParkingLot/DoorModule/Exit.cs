using System;
using System.Drawing;
using Camera;
using BLL.Interface;
using Model;
using OnlineChargesParkingLot.ViewModel;

namespace OnlineChargesParkingLot.DoorModule
{
    public class Exit : Door, IDoor
    {
        public Exit(ParkingLotInfo parkingLotInfo, Action callback)
        {

        }

        public void Execute(IdentificationInfo iInfo)
        {
            bool ret = Compared(iInfo.LicensePlateNumber, iInfo.IdentificationTime);
            if (ret)
            {
                return;
            }

            IEnteranceRecordService enteranceRecordService = BLL.Container.Container.Resolve<IEnteranceRecordService>();
            EnteranceRecord enteranceRecord = enteranceRecordService.Query(iInfo.LicensePlateNumber);
            IOwnerInfoService ownerInfoService = BLL.Container.Container.Resolve<IOwnerInfoService>();
            OwnerInfo ownerInfo = ownerInfoService.Query(iInfo.LicensePlateNumber);

        }
    }
}