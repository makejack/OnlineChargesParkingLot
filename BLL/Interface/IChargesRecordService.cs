using System;
using System.Collections.Generic;
using Model;

namespace BLL.Interface
{
    public interface IChargesRecordService : IBaseService<ChargesRecord>
    {
        ChargesRecord Query(string licensePlateNumber);

        IEnumerable<ChargesRecord> Query(string licensePlateNumber, DateTime now);
    }
}