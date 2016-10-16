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
    public class ClienteController : BaseController<Customer>
    {
        // GET: Fabrica/Cliente
        public ActionResult Index()
        {
            return View(_repositorio.PaginadoLista((x => x.Id), 1, 15));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            _repositorio.Agregar(customer);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var customer = _repositorio.ObtenerId(x => x.Id == id);
            if (customer == null) return RedirectToAction("Index");
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            _repositorio.Editar(customer);
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var customer = _repositorio.ObtenerId(x => x.Id == id);
            if (customer == null) return RedirectToAction("Index");
            return View(customer);
        }

        [HttpPost]
        public ActionResult Delete(Customer customer)
        {
            customer = _repositorio.ObtenerId(x => x.Id == customer.Id);
            _repositorio.Eliminar(customer);
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var customer = _repositorio.ObtenerId(x => x.Id == id);
            if (customer == null) return RedirectToAction("Index");
            return View(customer);

        }
    }
}