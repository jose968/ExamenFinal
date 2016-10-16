using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionWeb.Repositorio
{
    public class BaseRepositorio<T> : IRepositorio<T> where T : class
    {

        protected ContextoWebDb db;

        public BaseRepositorio()
        {
            db = new ContextoWebDb();
        }

        public BaseRepositorio(ContextoWebDb webcontext)
        {
            db = webcontext;
        }

        public int Agregar(T entidad)
        {
            db.Entry(entidad).State = EntityState.Added;
            return db.SaveChanges();
        }

        public int Editar(T entidad)
        {
            db.Entry(entidad).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public int Eliminar(T entidad)
        {
            db.Entry(entidad).State = EntityState.Deleted;
            return db.SaveChanges();
        }

        public IEnumerable<T> ListaId(Expression<Func<T, bool>> igualar)
        {
            return db.Set<T>().Where(igualar);
        }

        public T ObtenerId(Expression<Func<T, bool>> igualar)
        {
            return db.Set<T>().FirstOrDefault(igualar);
        }

        public List<T> ObtenerLista()
        {
            return db.Set<T>().ToList();
        }

        public IEnumerable<T> OrdenarListaFechaTamano(Expression<Func<T, DateTime>> igualar, int tamano)
        {
            return db.Set<T>().OrderByDescending(igualar).Take(tamano);
        }

        public IEnumerable<T> PaginadoLista(Expression<Func<T, int>> igualar, int pagina, int tamano)
        {
            return db.Set<T>().OrderByDescending(igualar).Pagina(pagina, tamano);
        }
    }
}
