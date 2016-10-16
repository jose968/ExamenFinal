using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicacionWeb.Areas.Fabrica.Controllers;
using Xunit;
using FluentAssertions;
using System.Web.Mvc;
using AplicacionWeb.Modelo;
using AplicacionWeb.Repositorio;

namespace AplicacionWeb.Prueba.Controllers
{
    public class ClienteControllerTest
    {
        private ClienteController controller;


        //public ClienteControllerTest()
        //{
        //    controller = new ClienteController(new BaseRepositorio<Customer>());
        //}

        [Fact(DisplayName = "ListActionWithEmptyParameters")]
        public void ListActionWithEmptyParameters()
        {
            var result = controller.Lista(null, null) as PartialViewResult;
            result.ViewName.Should().Be("_Lista");

            var modelCount = (IEnumerable<Customer>)result.Model;
            modelCount.Count().Should().Be(15);
        }

    }
}
