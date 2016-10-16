using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionWeb.Filters;
using AplicacionWeb.Repositorio;


namespace AplicacionWeb.Areas.Fabrica.Controllers
{
    [ControlAuditoria]
    [ControlExcepcion]
    public class BaseController<T> : Controller
              where T : class
    {
        // GET: Personnel/PersonBase
        protected IRepositorio<T> _repositorio;

        public BaseController()
        {
            _repositorio = new BaseRepositorio<T>();
        }
    }
}