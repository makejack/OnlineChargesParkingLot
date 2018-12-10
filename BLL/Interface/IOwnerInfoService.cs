using Model;


namespace BLL.Interface
{
    public interface IOwnerInfoService : IBaseService<OwnerInfo>
    {
        OwnerInfo Query(string licensePlateNumber);
    }
}