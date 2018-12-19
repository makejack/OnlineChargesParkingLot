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
    [DependencyRegister]
    public class ChargesRecordService : BaseService<ChargesRecord>, IChargesRecordService
    {
        public override void SetDal()
        {
            base.Dal = new ChargesRecordDAL();
        }

        public ChargesRecord Query(string licensePlateNumber)
        {
            IChargesRecordDAL chargesRecordDAL = DAL.Container.Container.Resolve<IChargesRecordDAL>();
            return chargesRecordDAL.Query(licensePlateNumber);
        }

        public IEnumerable<ChargesRecord> Query(string licensePlateNumber, DateTime now)
        {
            IChargesRecordDAL chargesRecordDAL = DAL.Container.Container.Resolve<IChargesRecordDAL>();
            return chargesRecordDAL.Query(licensePlateNumber, now);
        }
    }
}
