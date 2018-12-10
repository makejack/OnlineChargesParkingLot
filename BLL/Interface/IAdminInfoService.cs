using System;
using Model;

namespace BLL.Interface
{
    public interface IAdminInfoService : IBaseService<AdminInfo>
    {
        AdminInfo Query(string account, string pwd);
    }
}