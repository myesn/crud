using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Upo.CRUD.BasicProvider
{
    public abstract class BasicProvider<T> : IBasicProvider<T> where T : class
    {
        private readonly IBasicStore _store;
        public Type EntityType { get; set; }

        public BasicProvider(IBasicStore store)
        {
            this._store = store;
        }

        public virtual async Task<T> FindAsync(object[] keys)
        {
            return await GetSet().FindAsync(keys).ConfigureAwait(false);
        }

        public virtual IQueryable<T> Query()
        {
            return (IQueryable<T>)QueryableOfTypeMethod
                  .MakeGenericMethod(this.EntityType)
                  .Invoke(null, new object[] { GetSet() });
        }

        private DbSet<T> GetSet() =>
            (DbSet<T>)_store.DbContext.Set(typeof(T).FullName);

        private static MethodInfo QueryableOfTypeMethod = typeof(Queryable).GetMethod(nameof(Queryable.OfType));
    }
}
