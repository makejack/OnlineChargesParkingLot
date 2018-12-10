using System;
using System.Drawing;
using Camera;
using Model;
using BLL;
using BLL.Interface;
using BLL.Container;
using OnlineChargesParkingLot.Interface;
using OnlineChargesParkingLot.Model;

namespace OnlineChargesParkingLot.Doors
{
    public class Enter : IDoor
    {
        private Action m_CallBack;

        public Enter(Action callback)
        {
            m_CallBack = callback;
        }

        /// <summary>
        /// 当前车牌
        /// </summary>
        private EnterModel CurrentVehicle { get; set; }


        public void Execute(string licensePlateNumber, LicensePlateTypes licensePlateType, Color licensePlateColor, DateTime identificationTime, Image pImage, Image vImage)
        {
            if (CurrentVehicle != null)
            {
                if (CurrentVehicle.LicensePlateNumber == licensePlateNumber)
                {
                    TimeSpan timeSpan = identificationTime - CurrentVehicle.IdentificationTime;
                    if (timeSpan.TotalSeconds < 30)
                    {
                        //同一辆车不处理
                        //更新时间
                        CurrentVehicle.IdentificationTime = identificationTime;
                        return;
                    }
                }
            }
            EnterModel enterModel = new EnterModel(licensePlateNumber, identificationTime, licensePlateType, licensePlateColor, pImage, vImage);
            CurrentVehicle = enterModel;
            IEnteranceRecordService enteranceRecordService = BLL.Container.Container.Resolve<IEnteranceRecordService>();
            EnteranceRecord enteranceRecord = enteranceRecordService.Query(licensePlateNumber);
            IOwnerInfoService ownerInfoService = BLL.Container.Container.Resolve<IOwnerInfoService>();
            OwnerInfo ownerInfo = ownerInfoService.Query(licensePlateNumber);
            if (ownerInfo != null)
            {
                if (ownerInfo.StopTime.Date >= identificationTime.Date)
                {
                    TimeSpan timeSpan = ownerInfo.StopTime.Date - identificationTime.Date;
                    if (timeSpan.Days <= 5) //播报语音
                    {

                    }
                }
                else //过期
                {

                }
            }

            if (enteranceRecord == null)
            {
                enteranceRecord = new EnteranceRecord(licensePlateNumber, identificationTime, (int)licensePlateType);
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