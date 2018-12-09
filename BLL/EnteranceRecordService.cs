using DAL;
using Model;
using BLL.Interface;

namespace BLL
{
    public class EnteranceRecordService : BaseService<EnteranceRecord>, IEnteranceRecordService, IDependency
    {
        public override void SetDal()
        {
            base.Dal = new EnteranceRecordDAL();
        }
    }
}