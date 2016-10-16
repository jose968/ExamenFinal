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
    public class ProveedorController : BaseController<Supplier>
    {
        // GET: Fabrica/Proveedor
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
        public ActionResult Crear(Supplier proveedor)
        {
            if (!ModelState.IsValid) return View(proveedor);
            _repositorio.Agregar(proveedor);
            return RedirectToAction("Inicio");
        }

        public ActionResult Editar(int id)
        {
            var proveedor = _repositorio.ObtenerId(x => x.Id == id);
            if (proveedor == null) return RedirectToAction("Index");
            return PartialView("_Editar", proveedor);
        }

        [HttpPost]
        public ActionResult Editar(Supplier proveedor)
        {
            if (!ModelState.IsValid) return PartialView("_Editar", proveedor);
            _repositorio.Editar(proveedor);
            return RedirectToAction("Inicio");
        }


        public ActionResult Eliminar(int id)
        {
            var proveedor = _repositorio.ObtenerId(x => x.Id == id);
            if (proveedor == null) return RedirectToAction("Index");
            return PartialView("_Eliminar", proveedor);
        }

        [HttpPost]
        public ActionResult Eliminar(Supplier proveedor)
        {
            proveedor = _repositorio.ObtenerId(x => x.Id == proveedor.Id);
            _repositorio.Eliminar(proveedor);
            return RedirectToAction("Inicio");
        }


        public ActionResult Detalle(int id)
        {
            var proveedor = _repositorio.ObtenerId(x => x.Id == id);
            if (proveedor == null) return RedirectToAction("Index");
            return PartialView("_Detalle", proveedor);

        }
    }
}