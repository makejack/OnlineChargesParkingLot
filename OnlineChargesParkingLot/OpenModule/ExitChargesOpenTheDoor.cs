using System;
using System.Linq;
using Model;
using BLL.Interface;
using OnlineChargesParkingLot.ViewModel;
using OnlineChargesParkingLot.ChargesModule;
using System.Collections.Generic;

namespace OnlineChargesParkingLot.OpenModule
{
    public class ExitChargesOpenTheDoor : BaseOperating, IExitDoor
    {

        private ParkingLotInfo m_ParkingLotInfo;
        private Action PlateInfoCallBack { get; set; }
        private ICharges ChargesModule { get; set; }

        public ExitChargesOpenTheDoor(ParkingLotInfo parkingLotInfo, Action callback)
        {
            this.m_ParkingLotInfo = parkingLotInfo;
            this.PlateInfoCallBack = callback;

            switch (m_ParkingLotInfo.ChargeMode)
            {
                case 0:

                    break;
                case 1:

                    break;
                case 2:

                    break;
            }
        }

        public void Execute(IdentificationInfo iInfo, EnteranceRecord eRecord, OwnerInfo oInfo)
        {
            if (eRecord == null && oInfo == null)
            {
                PlateInfoCallBack();
                return;
            }
            else
            {
                string userType = "临时车辆";
                int day = 255;
                bool openTheDoor = true;
                bool charges = true;
                IChargesRecordService chargesRecordService = BLL.Container.Container.Resolve<IChargesRecordService>();
                IEnumerable<ChargesRecord> chargesRecords = chargesRecordService.Query(iInfo.LicensePlateNumber, DateTime.Today);
                double money = chargesRecords.Sum(e => e.ChargeAmount);
                if (oInfo != null)
                {
                    charges = false;
                    if (oInfo.PlateType == 0) //月租车辆
                    {
                        userType = "月租车辆";
                        day = SurplusDays(oInfo.StopTime);
                        if (day == 0)
                        {
                            //过期
                            userType += "（过期）";
                            charges = true;
                        }
                        //开门
                    }
                    else if (oInfo.PlateType == 1)//固定车辆
                    {
                        userType = "固定车辆";
                    }
                    else if (oInfo.PlateType == 2) //定距卡车辆
                    {
                        userType = "定距卡车辆";
                    }

                    if (oInfo.UserType == 1) //黑名单
                    {
                        //不开门
                        userType += "（黑名单）";

                        openTheDoor = false;
                    }
                }

                if (openTheDoor)
                {
                }
                else
                {

                }
            }

            PlateInfoCallBack();
        }
    }
}