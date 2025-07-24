using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Context;
using Store.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork<StoreContext>
    {
        private GameRepository? _gameRepository;
        private PlatformRepository? _platformRepository;
        private GamePlatformRepository? _gamePlatformRepository;
        private GenreRepository? _genreRepository;
        private GameGenreRepository? _gameGenreRepository;
        public UnitOfWork(StoreContext context)
        {
            Context = context;
        }


        public StoreContext Context { get; }
        public GameRepository GameRepository
        {
            get { return _gameRepository ??= new GameRepository(Context); }
        }

        public PlatformRepository PlatformRepository
        {
            get { return _platformRepository ??= new PlatformRepository(Context); }
        }

        public GamePlatformRepository GamePlatformRepository
        {
            get { return _gamePlatformRepository ??= new GamePlatformRepository(Context); }
        }

        public GameGenreRepository GameGenreRepository
        {
            get { return _gameGenreRepository ??= new GameGenreRepository(Context); }
        }

        public GenreRepository GenreRepository
        {
            get { return _genreRepository ??= new GenreRepository(Context); }
        }

        public async Task CreateTransactionAsync()
        {
            await Context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await Context.Database.CommitTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            await Context.Database.RollbackTransactionAsync();
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
