using DAL;
using Model;
using BLL.Interface;
using DAL.Interface;

namespace BLL
{
    public class EnteranceRecordService : BaseService<EnteranceRecord>, IEnteranceRecordService, IDependency
    {
        public override void SetDal()
        {
            base.Dal = new EnteranceRecordDAL();
        }

        public EnteranceRecord Query(string licensePlateNumber)
        {
            IEnteranceRecordDAL enteranceRecordDAL = DAL.Container.Container.Resolve<IEnteranceRecordDAL>();
            return enteranceRecordDAL.Query(licensePlateNumber);
        }
    }
}