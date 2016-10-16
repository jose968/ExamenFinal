using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionWeb.Repositorio
{
    public interface IRepositorio<T>
    {
        int Agregar(T entidad);
        int Editar(T entidad);
        int Eliminar(T entidad);
        List<T> ObtenerLista();

        T ObtenerId(Expression<Func<T, bool>> igualar);

        IEnumerable<T> OrdenarListaFechaTamano(Expression<Func<T, DateTime>> igualar, int tamano);

        IEnumerable<T> PaginadoLista(Expression<Func<T, int>> igualar, int pagina, int tamano);

        IEnumerable<T> ListaId(Expression<Func<T, bool>> igualar);


    }
}
