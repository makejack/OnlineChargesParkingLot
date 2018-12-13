using System;
using Model;

namespace DAL.Interface
{
    public interface IChargeRecordDAL : IBaseDAL<ChargeRecord>
    {
        ChargeRecord Query(string licensePlateNumber);
    }
}