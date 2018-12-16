using System;
using System.Drawing;
using Model;
using BLL.Interface;
using Camera;
using LogHelper;
using OnlineChargesParkingLot.OpenModule;
using OnlineChargesParkingLot.ViewModel;

namespace OnlineChargesParkingLot.DoorModule
{
    public class Enter : Door, IDoor
    {
        private IEnterDoor EnterDoor { get; set; }

        public Enter(ParkingLotInfo parkingLotInfo, Action<EnterVehicleInfo> callback)
        {
            switch (parkingLotInfo.OpenMode)
            {
                case 0: //ʶ�𼴿�բ
                    EnterDoor = new EnterIdentificationOpening(callback);
                    break;
                case 1: //�շѿ�բ
                    EnterDoor = new EnterChargesOpenTheDoor(callback);
                    break;
                case 2: //�̶��û���բ
                    EnterDoor = new EnterFixedUserOpenTheDoor(callback);
                    break;
            }
        }

        public void Execute(IdentificationInfo iInfo)
        {
            bool ret = Compared(iInfo.LicensePlateNumber, iInfo.IdentificationTime);
            if (ret)
            {
                return;
            }

            IEnteranceRecordService enteranceRecordService = BLL.Container.Container.Resolve<IEnteranceRecordService>();
            EnteranceRecord enteranceRecord = null;
            OwnerInfo ownerInfo = null;
            ChargesRecord chargeRecord = null;
            try
            {
                if (iInfo.LicensePlateNumber != "ABCDEF")
                {
                    enteranceRecord = enteranceRecordService.Query(iInfo.LicensePlateNumber);

                    IOwnerInfoService ownerInfoService = BLL.Container.Container.Resolve<IOwnerInfoService>();
                    ownerInfo = ownerInfoService.Query(iInfo.LicensePlateNumber);

                    //��ȡ��¼�еĳ������� �� С�� �г�
                    IChargesRecordService chargeRecordService = BLL.Container.Container.Resolve<IChargesRecordService>();
                    chargeRecord = chargeRecordService.Query(iInfo.LicensePlateNumber);
                }
                EnterDoor.Execute(iInfo, ownerInfo);
            }
            finally
            {
                try
                {
                    if (enteranceRecord == null)
                    {
                        int vehicleType = 0;
                        if (iInfo.LicensePlateType == LicensePlateTypes.LT_YELLOW || iInfo.LicensePlateType == LicensePlateTypes.LT_YELLOW2)
                        {
                            vehicleType = 2;
                        }
                        if (chargeRecord != null)
                        {
                            if (vehicleType != chargeRecord.VehicleType)
                            {
                                vehicleType = chargeRecord.VehicleType;
                            }
                        }
                        enteranceRecord = new EnteranceRecord(iInfo.LicensePlateNumber, iInfo.IdentificationTime, vehicleType);
                        enteranceRecordService.Add(enteranceRecord);
                    }
                    else
                    {
                        enteranceRecord.EntranceTime = iInfo.IdentificationTime;
                        enteranceRecordService.Update(enteranceRecord);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message, ex);
                }
            }
        }
    }
}