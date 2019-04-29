using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Upo.CRUD.BasicMutableService
{
    public abstract class MutableBasicService<T> : IMutableBasicService<T> where T : class
    {
        private readonly IBasicStore _store;

        public MutableBasicService(IBasicStore store)
        {
            this._store = store;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            GetSet().Add(entity);
            await _store.SaveChangesAsync().ConfigureAwait(false);

            return entity;
        }

        public virtual async Task<T> DeleteAsync(object[] keys)
        {
            var entity = await GetSet().FindAsync(keys).ConfigureAwait(false);
            if (entity == null)
                return null;

            GetSet().Remove(entity);
            await _store.SaveChangesAsync().ConfigureAwait(false);

            return entity;
        }

        public virtual async Task<T> UpdateAsync(object[] keys, IDictionary<string, object> updateProperties)
        {
            var entity = await GetSet().FindAsync(keys).ConfigureAwait(false);
            if (entity == null)
                throw new KeyNotFoundException($"{typeof(T).Name} not found");

            foreach (var updateProperty in updateProperties)
            {
                var property = entity.GetType().GetProperty(updateProperty.Key);
                property.SetValue(entity, updateProperty.Value);
            }
            GetSet().Update(entity);
            await _store.SaveChangesAsync().ConfigureAwait(false);

            return entity;
        }


        private DbSet<T> GetSet() =>
            (DbSet<T>)_store.DbContext.Set(typeof(T).FullName);
    }
}
