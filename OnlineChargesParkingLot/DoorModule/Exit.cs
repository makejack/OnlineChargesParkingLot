using System;
using System.Drawing;
using Camera;
using BLL.Interface;
using Model;
using OnlineChargesParkingLot.ViewModel;
using OnlineChargesParkingLot.OpenModule;

namespace OnlineChargesParkingLot.DoorModule
{
    public class Exit : Door, IDoor
    {
        private IExitDoor ExitDoor { get; set; }

        public Exit(ParkingLotInfo parkingLotInfo, Action callback)
        {
            switch (parkingLotInfo.OpenMode)
            {
                case 0: //识别即开闸
                    ExitDoor = new ExitIdentificationOpening(callback);
                    break;
                case 1: //收费开闸
                    ExitDoor = new ExitChargesOpenTheDoor(parkingLotInfo, callback);
                    break;
                case 2: //固定用户开闸
                    ExitDoor = new ExitFixedUserOpenTheDoor(callback);
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

            //获取入场的信息
            IEnteranceRecordService enteranceRecordService = BLL.Container.Container.Resolve<IEnteranceRecordService>();
            EnteranceRecord enteranceRecord = enteranceRecordService.Query(iInfo.LicensePlateNumber);
            //获取车主信息
            IOwnerInfoService ownerInfoService = BLL.Container.Container.Resolve<IOwnerInfoService>();
            OwnerInfo ownerInfo = ownerInfoService.Query(iInfo.LicensePlateNumber);
            ExitDoor.Execute(iInfo, enteranceRecord, ownerInfo);
        }
    }
}