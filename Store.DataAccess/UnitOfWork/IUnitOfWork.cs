using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.UnitOfWork
{
    public interface IUnitOfWork<out StoreContext> : IDisposable where StoreContext : DbContext
    {
        GameRepository GameRepository { get; }
        PlatformRepository PlatformRepository { get; }
        GamePlatformRepository GamePlatformRepository { get; }
        GameGenreRepository GameGenreRepository { get; }
        GenreRepository GenreRepository { get; }
        //The following Property is going to hold the context object
        StoreContext Context { get; }
        //Start the database Transaction
        Task CreateTransactionAsync();
        //Commit the database Transaction
        Task CommitAsync();
        //Rollback the database Transaction
        Task RollbackAsync();
        //DbContext Class SaveChanges method
        Task SaveAsync();
    }
}
