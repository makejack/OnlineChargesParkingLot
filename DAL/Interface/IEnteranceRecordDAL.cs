using System;
using Model;

namespace DAL.Interface
{
    public interface IEnteranceRecordDAL : IBaseDAL<EnteranceRecord>
    {
        EnteranceRecord Query(string licensePlateNumber);
    }
}