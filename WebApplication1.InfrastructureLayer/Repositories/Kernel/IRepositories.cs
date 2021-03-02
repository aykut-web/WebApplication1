using System;
using System.Collections.Generic;
using System.Text;
using WebApplication1.DomainLayer.Entities.Abstraction;

namespace WebApplication1.InfrastructureLayer.Repositories.Kernel
{
    public interface IRepositories<T> where T: CoreEntity
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
