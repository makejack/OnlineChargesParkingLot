using Model;

namespace BLL.Interface
{
    public interface IChargeRecordService : IBaseService<ChargeRecord>
    {
        ChargeRecord Query(string licensePlateNumber);
    }
}