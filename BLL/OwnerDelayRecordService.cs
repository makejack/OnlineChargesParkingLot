using System;
using BLL.Interface;
using Model;
using DAL;

namespace BLL
{
    [DependencyRegister]
    public class OwnerDelayRecordService : BaseService<OwnerDelayRecord>, IOwnerDelayRecordService
    {
        public override void SetDal()
        {
            base.Dal = new OwnerDelayRecordDAL();
        }
    }
}