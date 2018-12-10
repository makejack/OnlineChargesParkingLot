using System;
using Model;

namespace DAL.Interface
{
    public interface IOwnerInfoDAL : IBaseDAL<OwnerInfo>
    {
        OwnerInfo Query(string licensePlateNumber);
    }
}