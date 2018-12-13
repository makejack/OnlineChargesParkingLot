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
        private IEnterDoor OpenOperating { get; set; }

        private ParkingLotInfo m_ParkingLotInfo;

        public Enter(ParkingLotInfo parkingLotInfo, Action<EnterVehicleInfo> callback)
        {
            m_ParkingLotInfo = parkingLotInfo;

            switch (parkingLotInfo.OpenMode)
            {
                case 0: //识别即开闸
                    OpenOperating = new EnterIdentificationOpening(callback);
                    break;
                case 1: //收费开闸
                    OpenOperating = new EnterChargesOpenTheDoor(callback);
                    break;
                case 2: //固定用户开闸
                    OpenOperating = new EnterFixedUserOpenTheDoor(callback);
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
            ChargeRecord chargeRecord = null;
            try
            {
                if (iInfo.LicensePlateNumber != "ABCDEF")
                {
                    enteranceRecord = enteranceRecordService.Query(iInfo.LicensePlateNumber);

                    IOwnerInfoService ownerInfoService = BLL.Container.Container.Resolve<IOwnerInfoService>();
                    ownerInfo = ownerInfoService.Query(iInfo.LicensePlateNumber);

                    //获取记录中的车辆类型 大车 小车 中车
                    IChargeRecordService chargeRecordService = BLL.Container.Container.Resolve<IChargeRecordService>();
                    chargeRecord = chargeRecordService.Query(iInfo.LicensePlateNumber);
                }

                string userName = string.Empty;
                string userType = "临时车辆";
                //string vehicleType = VehicleTypeToStr(iInfo.LicensePlateType);
                int day = 255;
                if (ownerInfo != null)
                {
                    if (ownerInfo.PlateType == 0) //月租车辆
                    {
                        userType = "月租车辆";
                        day = SurplusDays(ownerInfo.StopTime);
                        if (day == 0)
                        {
                            //过期
                            userType += "（过期）";
                        }
                        //开门
                    }
                    else if (ownerInfo.PlateType == 1)//固定车辆
                    {
                        userType = "固定车辆";
                    }
                    else if (ownerInfo.PlateType == 2) //定距卡车辆
                    {
                        userType = "定距卡车辆";
                    }

                    if (ownerInfo.UserType == 1) //黑名单
                    {
                        //不开门
                        userType += "（黑名单）";
                    }
                }

                switch (m_ParkingLotInfo.OpenMode)
                {
                    case 0:

                        break;
                    case 1:

                        break;
                    case 2:

                        break;
                }

                OpenOperating.Execute(iInfo, ownerInfo);
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