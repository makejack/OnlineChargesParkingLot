using System.Collections;
using System.Collections.Generic;

namespace BLL.Interface
{
    public interface IBaseService<T> where T : class, new()
    {
        int Add(T t);

        void Update(T t);

        void Delete (T t);

        IEnumerable<T> GetModels();
    }
}