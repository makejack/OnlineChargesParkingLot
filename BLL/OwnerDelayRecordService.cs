using System;
using BLL.Interface;
using Model;
using DAL;

namespace BLL
{
    public class OwnerDelayRecordService : BaseService<OwnerDelayRecord>, IOwnerDelayRecordService, IDependency
    {
        public override void SetDal()
        {
            base.Dal = new OwnerDelayRecordDAL();
        }
    }
}