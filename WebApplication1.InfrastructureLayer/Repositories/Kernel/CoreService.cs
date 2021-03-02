using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.DomainLayer.Entities.Abstraction;

using WebApplication1.InfrastructureLayer.Context;

namespace WebApplication1.InfrastructureLayer.Repositories.Kernel
{
    public class CoreService<T> : IRepositories<T> where T: CoreEntity
    {
        private readonly ProjectContext _context;
        private readonly DbSet<T> _entities;

        public CoreService(ProjectContext context)
        {
            this._context = context;
            this._entities = _context.Set<T>();
            
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void Delete(T entity)
        {
            //_context.Entry<T>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;


            
                _entities.Remove(entity);
            
          
            

        }

        public  IEnumerable<T> GetAll()
        {
            
            return _entities.AsEnumerable();
        }

        public T GetById(Guid id)
        {
            return _entities.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }
    }
}
