using System;
using Model;

namespace DAL.Interface
{
    public interface IChargeRecordDAL : IBaseDAL<ChargeRecord>
    {
        int Query(string licensePlateNumber);
    }
}