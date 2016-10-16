﻿using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionWeb.Filters
{
    public class ControlAuditoria: ActionFilterAttribute
    {
        private static ILog Log { get; set; }

        ILog log = LogManager.GetLogger
            (
               System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
            );
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Message(filterContext, "OnActionExecuted");
            //var controller = filterContext.RouteData.Values["controller"].ToString();
            //var action = filterContext.RouteData.Values["action"].ToString();
            //var filterMethod = "OnActionExecuted";
            //log.Info($"{controller} in action {action} on {filterMethod}");
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        =>
            Message(filterContext, "OnResultExecuted");
        

        private void Message(string controller, string action, string filterMethod)
        {
            log.Info($"{controller} in action {action} on {filterMethod}");
        }

        private void Message(ControllerContext context, string filterMethod)
        {
            var controller = context.RouteData.Values["controller"].ToString();
            var action = context.RouteData.Values["action"].ToString();
            log.Info($"{controller} in action {action} on {filterMethod}");
        }
    }
}