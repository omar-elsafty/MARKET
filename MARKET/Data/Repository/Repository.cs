using MARKET.Models;
using MARKET.Services.Comunications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MARKET.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly DBMarket context;

        public Repository(DBMarket context)
        {
            this.context = context;
        }

        public async Task<EntityResponse<TEntity>> Add(TEntity entity)
        {
             await context.Set<TEntity>().AddAsync(entity);
            try
            {
                //should be handled
                 await context.SaveChangesAsync();
                return new EntityResponse<TEntity>(entity);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new EntityResponse<TEntity>($"An error occurred when saving the new element: {ex.Message}");
            }
            
        }

        public async Task<EntityResponse<TEntity>> Edit(TEntity entity)
        {
            var result = await context.Set<TEntity>().ContainsAsync(entity);
            if (!result)
            {
                return new EntityResponse<TEntity>("The element not found");
            }

            try
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return new EntityResponse<TEntity>(entity);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new EntityResponse<TEntity>($"An error occurred when updating the element: {ex.Message}");
            }
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<EntityResponse<TEntity>> Delete (int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return new EntityResponse<TEntity>("The element not found");
            }
            try
            {
                context.Set<TEntity>().Remove(entity);
                await context.SaveChangesAsync();
                return new EntityResponse<TEntity>(entity);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new EntityResponse<TEntity>($"An error occurred when deleting the element: {ex.Message}");
            }
        }
  
    }
}
