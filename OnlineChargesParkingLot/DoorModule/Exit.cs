using System;
using System.Drawing;
using Camera;
using BLL.Interface;
using Model;

namespace OnlineChargesParkingLot.DoorModule
{
    public class Exit : Door,IDoor
    {
        public Exit(ParkingLotInfo parkingLotInfo, Action callback) : base(parkingLotInfo, callback)
        {

        }

        public void Execute(string licensePlateNumber, LicensePlateTypes licensePlateType, Color licensePlateColor, DateTime identificationTime)
        {
            bool ret = Compared(licensePlateNumber, identificationTime);
            if (ret)
            {
                return;
            }

            IEnteranceRecordService enteranceRecordService = BLL.Container.Container.Resolve<IEnteranceRecordService>();
            EnteranceRecord enteranceRecord = enteranceRecordService.Query(licensePlateNumber);
            IOwnerInfoService ownerInfoService = BLL.Container.Container.Resolve<IOwnerInfoService>();
            OwnerInfo ownerInfo = ownerInfoService.Query(licensePlateNumber);

            if (enteranceRecord == null && ownerInfo == null)
            {
                CompleteCallback();
            }
            else
            {

            }
        }
    }
}