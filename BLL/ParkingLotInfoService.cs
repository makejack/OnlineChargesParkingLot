using System;
using Model;
using DAL;
using DAL.Interface;
using BLL.Interface;

namespace BLL
{
    [DependencyRegister]
    public class ParkingLotInfoService : BaseService<ParkingLotInfo>, IParkingLotInfoService
    {
        public override void SetDal()
        {
            base.Dal = new ParkingLotInfoDAL();
        }
    }
}