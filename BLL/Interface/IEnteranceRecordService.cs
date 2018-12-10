using Model;

namespace BLL.Interface
{
    public interface IEnteranceRecordService : IBaseService<EnteranceRecord>
    {
        EnteranceRecord Query(string licensePlateNumber);
    }
}