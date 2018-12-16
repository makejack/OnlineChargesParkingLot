using System;
using System.Collections.Generic;
using Model;

namespace DAL.Interface
{
    public interface IChargesRecordDAL : IBaseDAL<ChargesRecord>
    {
        ChargesRecord Query(string licensePlateNumber);

        IEnumerable<ChargesRecord> Query(string licensePlateNumber, DateTime now);
    }
}