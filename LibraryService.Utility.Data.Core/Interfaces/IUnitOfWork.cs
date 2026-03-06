using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryService.Utility.Data.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync();
    }
}
