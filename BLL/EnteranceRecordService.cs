using DAL;
using Model;
using BLL.Interface;
using DAL.Interface;

namespace BLL
{
    [DependencyRegister]
    public class EnteranceRecordService : BaseService<EnteranceRecord>, IEnteranceRecordService
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