using MARKET.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MARKET.Data.Repository
{
    public interface IRepository<TEntity>  
        where TEntity :class
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<EntityResponse<TEntity>> Add(TEntity entity);
        Task<EntityResponse<TEntity>> Edit(TEntity entity);
        Task<EntityResponse<TEntity>> Delete (int id);
    }
}
