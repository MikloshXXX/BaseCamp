using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlatHouse
{
    interface DAO<T>
    {
        public bool Insert(T entity);
        public bool Delete(int id);
        public bool Update(int id, T entity);
        public List<T> Get();
        public T Get(int id);

    }
}
