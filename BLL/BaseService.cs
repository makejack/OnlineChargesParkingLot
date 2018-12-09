using Model;
using System.Collections;
using System.Collections.Generic;
using DAL;
using DAL.Interface;

namespace BLL
{
    public abstract partial class BaseService<T> where T : class, new()
    {
        public BaseService()
        {
            SetDal();
        }

        public IBaseDAL<T> Dal { get; set; }

        public abstract void SetDal();

        public int Add(T t)
        {
            return Dal.Add(t);
        }

        public void Delete(T t)
        {
            Dal.Delete(t);
        }

        public IEnumerable<T> GetModels()
        {
            return Dal.GetModels();
        }

        public void Update(T t)
        {
            Dal.Update(t);
        }
    }
}