using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPIParameterBinding.Configuration;

namespace WebAPIParameterBinding
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            MyConfiguration.Register(config);


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
