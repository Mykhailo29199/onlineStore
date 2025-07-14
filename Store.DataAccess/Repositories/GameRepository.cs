using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Context;
using Store.DataAccess.Entities;
using Store.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories
{
    public class GameRepository : GenericRepository<GameEntity>
    {
        public GameRepository(StoreContext context) : base(context)
        {
        }

        //public asyc Task<bool> CheckIfExistKey()

        //(Guid id)
        //{
        //    return GetByIdAsync(id) == null;
        //}

}
}
