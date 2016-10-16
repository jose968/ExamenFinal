using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionWeb.Repositorio
{
    public static class ExtensionDb
    {

        public static IEnumerable<TSource> Pagina<TSource>(
            this IEnumerable<TSource> codigo,
            int pagina,
            int tamano)
        {
            return codigo.Skip((pagina - 1) * tamano).Take(tamano);

        }



    }
}
