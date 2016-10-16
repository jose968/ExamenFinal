using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using LightInject;

namespace AplicacionWeb
{
	public partial class Startup
	{
		public void ConfigInjector()
        {
            var container = new ServiceContainer();
            container.RegisterAssembly(Assembly.GetExecutingAssembly());
            container.RegisterAssembly("AplicacionWeb.Modelo*.dll");
            container.RegisterAssembly("AplicacionWeb.Repositorio*.dll");
            container.RegisterControllers();
            container.EnableMvc();
        }
	}
}