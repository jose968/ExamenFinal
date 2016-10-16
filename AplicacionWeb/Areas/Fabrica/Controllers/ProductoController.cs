using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionWeb.Filters;
using AplicacionWeb.Modelo;
using AplicacionWeb.Repositorio;

namespace AplicacionWeb.Areas.Fabrica.Controllers
{
    public class ProductoController : BaseController<Product>
    {
        // GET: Fabrica/Producto
        public ActionResult Inicio()
        {
            return View(_repositorio.PaginadoLista((x => x.Id), 1, 15));
        }


        public ActionResult Lista(int? pagina, int? tamano)
        {
            if (!pagina.HasValue || !tamano.HasValue)
            {
                pagina = 1;
                tamano = 15;
            }
            return PartialView("_Lista", _repositorio.PaginadoLista(x => x.Id, pagina.Value, tamano.Value));
        }

        public int PageSize(int pageSize)
        {
            if (pageSize <= 0) return 0;
            var totalRecords = _repositorio.ObtenerLista().Count;
            return totalRecords % pageSize > 0 ? (totalRecords / pageSize) + 1 : totalRecords / pageSize;
        }
        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Product producto)
        {
            if (!ModelState.IsValid) return View(producto);
            _repositorio.Agregar(producto);
            return RedirectToAction("Inicio");
        }

        public ActionResult Editar(int id)
        {
            var producto = _repositorio.ObtenerId(x => x.Id == id);
            if (producto == null) return RedirectToAction("Index");
            return PartialView("_Editar", producto);
        }

        [HttpPost]
        public ActionResult Editar(Product producto)
        {
            if (!ModelState.IsValid) return PartialView("_Editar", producto);
            _repositorio.Editar(producto);
            return RedirectToAction("Inicio");
        }


        public ActionResult Eliminar(int id)
        {
            var producto = _repositorio.ObtenerId(x => x.Id == id);
            if (producto == null) return RedirectToAction("Index");
            return PartialView("_Eliminar", producto);
        }

        [HttpPost]
        public ActionResult Eliminar(Product producto)
        {
            producto = _repositorio.ObtenerId(x => x.Id == producto.Id);
            _repositorio.Eliminar(producto);
            return RedirectToAction("Inicio");
        }


        public ActionResult Detalle(int id)
        {
            var producto = _repositorio.ObtenerId(x => x.Id == id);
            if (producto == null) return RedirectToAction("Index");
            return PartialView("_Detalle", producto);

        }
    }
}