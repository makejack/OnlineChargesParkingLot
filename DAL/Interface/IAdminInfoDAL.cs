using Model;

namespace DAL.Interface
{
    public interface IAdminInfoDAL : IBaseDAL<AdminInfo>
    {
        AdminInfo Query(string account, string pwd);
    }
}