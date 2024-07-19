using DemoBTL.Domain.InterfacReponsitories;
using DemoBTL.Infastructure.DataContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DemoBTL.Infastructure.ImplemenRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected IDbcontext _IDbcontext = null;
        protected DbSet<TEntity> _dbset;
        protected DbContext _Dbcontext;
        protected DbSet<TEntity> Dbset
        {
            get
            {
                if (_dbset == null)
                {
                    _dbset = _Dbcontext.Set<TEntity>() as DbSet<TEntity>;
                }
                return _dbset;
            }
        }
        public BaseRepository(IDbcontext dbcontext)
        {
            _IDbcontext = dbcontext;
            _Dbcontext = (DbContext)dbcontext;
        }




        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            IQueryable<TEntity> query = expression != null ? Dbset.Where(expression) : Dbset;
            return await query.CountAsync();
        }

        public async Task<int> CountAsync(string incude, Expression<Func<TEntity, bool>> expression = null)
        {
            IQueryable<TEntity> query;
            if (!string.IsNullOrEmpty(incude))
            {
                query = BuildQueryable(new List<string> { incude }, expression);
                return await query.CountAsync();
            }
            return await CountAsync(expression);
        }
        protected IQueryable<TEntity> BuildQueryable(List<string> incude, Expression<Func<TEntity, bool>> expression)
        {
            IQueryable<TEntity> query = Dbset.AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (incude != null && incude.Count > 0)
            {
                foreach (var item in incude)
                {
                    query = query.Include(item.Trim());
                }
            }
            return query;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            Dbset.Add(entity);
            await _IDbcontext.CommitChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> CreateAsync(IEnumerable<TEntity> entities)
        {
            Dbset.AddRange(entities);
            await _IDbcontext.CommitChangesAsync();
            return entities;
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {

            IQueryable<TEntity> query = expression != null ? Dbset.Where(expression) : Dbset;
            var DataEntity = query;
            if (DataEntity != null)
            {
                Dbset.RemoveRange(DataEntity);
                await _IDbcontext.CommitChangesAsync();
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            var dataEntity = await Dbset.FindAsync(id);
            if (dataEntity != null)
            {
                Dbset.Remove(dataEntity);
                await _IDbcontext.CommitChangesAsync();
            }
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            IQueryable<TEntity> query = expression != null ? Dbset.Where(expression) : Dbset;
            return query;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Dbset.FirstOrDefaultAsync(expression);
        }

        public async Task<TEntity> GetbyIdAsync(int id)
        {
            return await Dbset.FindAsync(id);
        }

        public async Task<TEntity> GetIdAAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return await Dbset.FirstOrDefaultAsync(expression);

        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _Dbcontext.Entry(entity).State = EntityState.Modified;
            await _IDbcontext.CommitChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                _Dbcontext.Entry(item).State = EntityState.Modified;
            }
            await _IDbcontext.CommitChangesAsync();
            return entities;
        }
    }
}
