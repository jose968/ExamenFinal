using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionWeb.Filters;
using AplicacionWeb.Modelo;
using AplicacionWeb.Repositorio;
using System.Net;

namespace AplicacionWeb.Areas.Fabrica.Controllers
{
    public class ClienteController : BaseController<Customer>
    {
        // GET: Fabrica/Cliente
    
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
        public ActionResult Crear(Customer cliente)
        {
            if (!ModelState.IsValid) return View(cliente);
            _repositorio.Agregar(cliente);
            return RedirectToAction("Inicio");
        }

        public ActionResult Editar(int id)
        {
            var cliente = _repositorio.ObtenerId(x => x.Id == id);
            if (cliente == null) return RedirectToAction("Index");
            return PartialView("_Editar", cliente);

            //var customer = _repositorio.ObtenerId(x => x.Id == id);
            //if (customer == null) return RedirectToAction("Index");
            //return View(customer);
        }

        [HttpPost]
        public ActionResult Editar(Customer cliente)
        {
            if (!ModelState.IsValid) return PartialView("_Editar", cliente);
            _repositorio.Editar(cliente);
            return RedirectToAction("Inicio");
            //if (!ModelState.IsValid) return View(customer);
            //_repositorio.Editar(customer);
            //return RedirectToAction("Index");
        }


        public ActionResult Eliminar(int id)
        {
            var cliente = _repositorio.ObtenerId(x => x.Id == id);
            if (cliente == null) return RedirectToAction("Index");
            return PartialView("_Eliminar", cliente);
        }

        [HttpPost]
        public ActionResult Eliminar(Customer cliente)
        {
            cliente = _repositorio.ObtenerId(x => x.Id == cliente.Id);
            _repositorio.Eliminar(cliente);
            return RedirectToAction("Inicio");
        }


        public ActionResult Detalle(int id)
        {
            var cliente = _repositorio.ObtenerId(x => x.Id == id);
            if (cliente == null) return RedirectToAction("Index");
            return PartialView("_Detalle", cliente);

        }
    }
}