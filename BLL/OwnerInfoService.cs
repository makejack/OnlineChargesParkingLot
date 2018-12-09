using Model;
using BLL.Interface;
using DAL;

namespace BLL.Properties
{
    public class OwnerInfoService : BaseService<OwnerInfo>, IOwnerInfoService, IDependency
    {
        public override void SetDal()
        {
            base.Dal = new OwnerInfoDAL();
        }
    }
}