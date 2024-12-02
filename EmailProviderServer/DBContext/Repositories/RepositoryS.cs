﻿using EmailProviderServer.DBContext.Repositories.Base;
using EmailProviderServer.DBContext.Repositories.Interfaces;
using EmailServiceIntermediate.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EmailProviderServer.DBContext.Repositories
{
    public class RepositoryS<TEntity> : IRepositoryS<TEntity>
        where TEntity : class, IEntity
    {
        private readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryS(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public IQueryable<TEntity> All()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> AllAsNoTracking()
        {
            return _dbSet.AsNoTracking();
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<TEntity> GetByID(int nId)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == nId);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public async Task<bool> CheckIfExists(int id)
        {
            var user = await GetByID(id);
            return user != null;
        }
    }
}