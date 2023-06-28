using Microsoft.EntityFrameworkCore;
using Rocky_DataAccess.Data;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rocky_DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        /// <summary>
        /// Добавлення записів
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Пошук записів
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find(int id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// Пошук підходячого першого запису
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includePropertis"></param>
        /// <param name="isTracking"></param>
        /// <returns></returns>
        public T FirstOrDefault(Expression<Func<T, bool>> filter = null, string includePropertis = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            //перевіряємо чи є параметер фільтр
            if (filter != null)
            {
                query = query.Where(filter);
            }
            //перевіряємо чи є параметри, переводимо у стрічку
            if (includePropertis != null)
            {
                foreach (var includeProp in includePropertis.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query.Include(includeProp);
                }
            }
            //запит не буде відслідковуватись
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Отримання всіх записів
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includePropertis"></param>
        /// <param name="isTracking"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includePropertis = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            //перевіряємо чи є параметер фільтр
            if (filter != null)
            {
                query = query.Where(filter);
            }
            //перевіряємо чи є параметри, переводимо у стрічку
            if(includePropertis != null)
            {
                foreach(var includeProp in includePropertis.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query.Include(includeProp);
                }
            }
            //перевіряємо чи є сортування
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            //запит не буде відслідковуватись
            if (!isTracking)
            {
                query = query.AsNoTracking();   
            }

            return query.ToList();
        }

        /// <summary>
        /// Видалення записів
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        /// <summary>
        /// Масове видалення
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

        /// <summary>
        /// Збереження запису
        /// </summary>
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
