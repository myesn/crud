using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Upo.CRUD
{
    public interface IMutableBasicService<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<T> DeleteAsync(object[] keys);
        Task<T> UpdateAsync(object[] keys, IDictionary<string, object> updateProperties);
    }
}
