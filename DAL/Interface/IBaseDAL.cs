using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Interface
{
    public partial interface IBaseDAL<T> where T : class, new()
    {
        int Add(T t);

        void Update(T t);

        void Delete(T t);

        IEnumerable<T> GetModels();


    }
}
