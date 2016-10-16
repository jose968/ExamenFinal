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
    public class OrdenElementoController : BaseController<OrderItem>
    {
        // GET: Fabrica/OrdenElemento
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
        public ActionResult Crear(OrderItem orden)
        {
            if (!ModelState.IsValid) return View(orden);
            _repositorio.Agregar(orden);
            return RedirectToAction("Inicio");
        }

        public ActionResult Editar(int id)
        {
            var orden = _repositorio.ObtenerId(x => x.Id == id);
            if (orden == null) return RedirectToAction("Index");
            return PartialView("_Editar", orden);
        }

        [HttpPost]
        public ActionResult Editar(OrderItem orden)
        {
            if (!ModelState.IsValid) return PartialView("_Editar", orden);
            _repositorio.Editar(orden);
            return RedirectToAction("Inicio");
        }


        public ActionResult Eliminar(int id)
        {
            var orden = _repositorio.ObtenerId(x => x.Id == id);
            if (orden == null) return RedirectToAction("Index");
            return PartialView("_Eliminar", orden);
        }

        [HttpPost]
        public ActionResult Eliminar(OrderItem orden)
        {
            orden = _repositorio.ObtenerId(x => x.Id == orden.Id);
            _repositorio.Eliminar(orden);
            return RedirectToAction("Inicio");
        }


        public ActionResult Detalle(int id)
        {
            var orden = _repositorio.ObtenerId(x => x.Id == id);
            if (orden == null) return RedirectToAction("Index");
            return PartialView("_Detalle", orden);

        }
    }
}