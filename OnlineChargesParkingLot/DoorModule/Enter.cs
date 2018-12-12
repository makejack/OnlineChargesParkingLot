using System;
using System.Drawing;
using Model;
using BLL.Interface;
using Camera;
using OnlineChargesParkingLot.OpenModule;

namespace OnlineChargesParkingLot.DoorModule
{
    public class Enter : Door, IDoor
    {
        public Enter(ParkingLotInfo parkingLotInfo, Action callback) : base(parkingLotInfo, callback)
        {
            switch (ParkingLot.OpenMode)
            {
                case 0: //识别即开闸
                    OpenOperating = new EnterIdentificationOpening();
                    break;
                case 1: //收费开闸
                    OpenOperating = new EnterChargesOpenTheDoor();
                    break;
                case 2: //固定用户开闸
                    OpenOperating = new EnterFixedUserOpenTheDoor();
                    break;
            }
        }

        public void Execute(string licensePlateNumber, LicensePlateTypes licensePlateType, Color licensePlateColor, DateTime identificationTime)
        {
            bool ret = Compared(licensePlateNumber, identificationTime);
            if (ret)
            {
                return;
            }

            IEnteranceRecordService enteranceRecordService = BLL.Container.Container.Resolve<IEnteranceRecordService>();
            EnteranceRecord enteranceRecord = null;
            try
            {
                if (licensePlateNumber != "ABCDEF")
                {
                    enteranceRecord = enteranceRecordService.Query(licensePlateNumber);

                    IOwnerInfoService ownerInfoService = BLL.Container.Container.Resolve<IOwnerInfoService>();
                    OwnerInfo ownerInfo = ownerInfoService.Query(licensePlateNumber);

                    OpenOperating.Execute(ownerInfo);

                    CompleteCallback();
                }
            }
            finally
            {
                if (enteranceRecord == null)
                {
                    enteranceRecord = new EnteranceRecord(licensePlateNumber, identificationTime, (int)licensePlateType);
                    //获取记录中的车辆类型 大车 小车 中车
                    IChargeRecordService chargeRecordService = BLL.Container.Container.Resolve<IChargeRecordService>();
                    int plateType = chargeRecordService.Query(licensePlateNumber);
                    if (enteranceRecord.VehicleType != plateType)
                    {
                        enteranceRecord.VehicleType = plateType;
                    }
                    enteranceRecordService.Add(enteranceRecord);
                }
                else
                {
                    enteranceRecord.EntranceTime = identificationTime;
                    enteranceRecordService.Update(enteranceRecord);
                }
            }
        }
    }
}