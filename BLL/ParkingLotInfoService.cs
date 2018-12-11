using System;
using Model;
using DAL;
using DAL.Interface;
using BLL.Interface;

namespace BLL
{
    public class ParkingLotInfoService : BaseService<ParkingLotInfo>, IParkingLotInfoService, IDependency
    {
        public override void SetDal()
        {
            base.Dal = new ParkingLotInfoDAL();
        }
    }
}