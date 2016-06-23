using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http.Formatting;
using Ninject;


using CachingService.DependencyContainer;
using CachingService.Business;
using CachingService.Business.Interfaces;

namespace CachingService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            var kernel = new StandardKernel();

            kernel.Bind<IDBListener>()
                .ToConstant<HubConfigurationManager>(new HubConfigurationManager());
            
            config.DependencyResolver = new NInjectDependencyResolver(kernel);
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter() { Indent = true });

        }
    }
}
