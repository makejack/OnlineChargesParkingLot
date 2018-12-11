using System;
using System.Drawing;
using Model;
using BLL.Interface;
using Camera;

namespace OnlineChargesParkingLot.DoorModule
{
    public class Enter : Door
    {
        public Enter(Action callback) : base(callback)
        {

        }

        public override void Execute(string licensePlateNumber, LicensePlateTypes licensePlateType, Color licensePlateColor, DateTime identificationTime, Image pImage, Image vImage)
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
            if (ownerInfo != null)
            {
                TimeSpan timeSpan = ownerInfo.StopTime.Date - identificationTime.Date;
                if (timeSpan.TotalDays >= 0)
                {

                }
                else
                {

                }
            }

            if (enteranceRecord == null)
            {
                enteranceRecord = new EnteranceRecord();
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