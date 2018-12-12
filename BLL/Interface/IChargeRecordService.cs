using Model;

namespace BLL.Interface
{
    public interface IChargeRecordService : IBaseService<ChargeRecord>
    {
        int Query(string licensePlateNumber);
    }
}