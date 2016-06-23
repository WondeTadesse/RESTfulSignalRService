//|---------------------------------------------------------------|
//|                   RESTFUL SIGNALR SERVICE                     |
//|---------------------------------------------------------------|
//|                     Developed by Wonde Tadesse                |
//|                        Copyright ©2015 - Present              |
//|---------------------------------------------------------------|
//|                   RESTFUL SIGNALR SERVICE                     |
//|---------------------------------------------------------------|
using System;
using System.Web.Http;
using Owin;

using Ninject;

using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin;
using Microsoft.Owin.Cors;

using RESTfulSignalRService.SignalRHubs;
using RESTfulSignalRService.Interfaces;
using RESTfulSignalRService.MessageBroadCaster;
using RESTfulSignalRService.DependencyContainer;



[assembly: OwinStartup(typeof(RESTfulSignalRService.Startup))]
namespace RESTfulSignalRService
{
    /// <summary>
    /// OWIN startup class
    /// </summary>
    public class Startup
    {
        #region Public Methods 

        /// <summary>
        /// Configuration value
        /// </summary>
        /// <param name="app">IAppBuilder value</param>
        public void Configuration(IAppBuilder app)
        {
            var kernel = new StandardKernel();

            // SignalR Hub DP resolver
            var signalRDependencyResolver = new NInjectSignalRDependencyResolver(kernel);

            // Register hub connection context
            kernel.Bind(typeof(IHubConnectionContext<dynamic>)).
                  ToMethod(context =>
                  signalRDependencyResolver.Resolve<IConnectionManager>().
                  GetHubContext<BroadCastHub>().Clients).
                  WhenInjectedInto<IBroadCast>();

            // Register message broadcaster class
            kernel.Bind<IBroadCast>().
                ToConstant<BroadCaster>(new BroadCaster());

            // IBroadcast DP resolver
            GlobalConfiguration.Configuration.DependencyResolver = new NInjectDependencyResolver(kernel);
          
            GlobalHost.Configuration.MaxIncomingWebSocketMessageSize = null; // Unlimited incoming message size

            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                map.RunSignalR(new HubConfiguration()
                {
                    EnableDetailedErrors = true,
                    Resolver = signalRDependencyResolver,
                });
            });
        }

        #endregion

    }
}
