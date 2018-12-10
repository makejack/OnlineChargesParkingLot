using DAL;
using Model;
using System;
using System.Data;
using BLL.Interface;
using DAL.Interface;

namespace BLL
{
    public class AdminInfoService : BaseService<AdminInfo>, IAdminInfoService, IDependency
    {
        public override void SetDal()
        {
            base.Dal = new AdminInfoDAL();
        }

        public AdminInfo Query(string account, string pwd)
        {
            IAdminInfoDAL dal = DAL.Container.Container.Resolve<IAdminInfoDAL>();
            return dal.Query(account, pwd);
        }
    }
}