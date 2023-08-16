using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Infra.Data.Contexts;

namespace UsuariosApp.Infra.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        public void Create(T entity)
        {
            using (var context = new DataContext())
            {
                context.Add(entity);
                context.SaveChanges();
            }
        }

        public void Update(T entity)
        {
            using (var context = new DataContext())
            {
                context.Update(entity);
                context.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            using (var context = new DataContext())
            {
                context.Remove(entity);
                context.SaveChanges();
            }
        }

        public List<T> GetAll()
        {
            using (var context = new DataContext())
            {
                return context.Set<T>().ToList();
            }
        }

        public T GetById(Guid id)
        {
            using (var context = new DataContext())
            {
                return context.Set<T>().Find(id);
            }
        }
    }
}
