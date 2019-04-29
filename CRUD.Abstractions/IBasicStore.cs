using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Upo.CRUD
{
    public interface IBasicStore
    {
        DbContext DbContext { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
