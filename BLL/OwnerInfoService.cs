using Model;
using BLL.Interface;
using DAL;
using DAL.Interface;

namespace BLL
{
    [DependencyRegister]
    public class OwnerInfoService : BaseService<OwnerInfo>, IOwnerInfoService
    {
        public override void SetDal()
        {
            base.Dal = new OwnerInfoDAL();
        }

        public OwnerInfo Query(string licensePlateNumber)
        {
            IOwnerInfoDAL ownerInfoDAL = DAL.Container.Container.Resolve<IOwnerInfoDAL>();
            return ownerInfoDAL.Query(licensePlateNumber);
        }
    }
}