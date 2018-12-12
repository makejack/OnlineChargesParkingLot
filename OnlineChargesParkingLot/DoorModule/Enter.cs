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
                case 0: //ʶ�𼴿�բ
                    OpenOperating = new EnterIdentificationOpening();
                    break;
                case 1: //�շѿ�բ
                    OpenOperating = new EnterChargesOpenTheDoor();
                    break;
                case 2: //�̶��û���բ
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
                    //��ȡ��¼�еĳ������� �� С�� �г�
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