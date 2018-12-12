using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Interface;
using DAL.Interface;
using Model;
using DAL;


namespace BLL
{
    public class ChargeRecordService : BaseService<ChargeRecord>, IChargeRecordService, IDependency
    {
        public override void SetDal()
        {
            base.Dal = new ChargeRecordDAL();
        }

        public int Query(string licensePlateNumber)
        {
            IChargeRecordDAL chargeRecordDAL = DAL.Container.Container.Resolve<IChargeRecordDAL>();
            return chargeRecordDAL.Query(licensePlateNumber);
        }
    }
}
