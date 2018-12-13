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
                case 0: //ʶ�𼴿�բ
                    OpenOperating = new EnterIdentificationOpening(callback);
                    break;
                case 1: //�շѿ�բ
                    OpenOperating = new EnterChargesOpenTheDoor(callback);
                    break;
                case 2: //�̶��û���բ
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

                    //��ȡ��¼�еĳ������� �� С�� �г�
                    IChargeRecordService chargeRecordService = BLL.Container.Container.Resolve<IChargeRecordService>();
                    chargeRecord = chargeRecordService.Query(iInfo.LicensePlateNumber);
                }

                string userName = string.Empty;
                string userType = "��ʱ����";
                //string vehicleType = VehicleTypeToStr(iInfo.LicensePlateType);
                int day = 255;
                if (ownerInfo != null)
                {
                    if (ownerInfo.PlateType == 0) //���⳵��
                    {
                        userType = "���⳵��";
                        day = SurplusDays(ownerInfo.StopTime);
                        if (day == 0)
                        {
                            //����
                            userType += "�����ڣ�";
                        }
                        //����
                    }
                    else if (ownerInfo.PlateType == 1)//�̶�����
                    {
                        userType = "�̶�����";
                    }
                    else if (ownerInfo.PlateType == 2) //���࿨����
                    {
                        userType = "���࿨����";
                    }

                    if (ownerInfo.UserType == 1) //������
                    {
                        //������
                        userType += "����������";
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