//|---------------------------------------------------------------|
//|                   RESTFUL SIGNALR SERVICE                     |
//|---------------------------------------------------------------|
//|                     Developed by Wonde Tadesse                |
//|                        Copyright ©2015 - Present              |
//|---------------------------------------------------------------|
//|                   RESTFUL SIGNALR SERVICE                     |
//|---------------------------------------------------------------|
using Microsoft.AspNet.SignalR;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTfulSignalRService.DependencyContainer
{
    /// <summary>
    /// SignalR NInject dependency resolver class 
    /// </summary>
    public class NInjectSignalRDependencyResolver : DefaultDependencyResolver
    {
        #region Private Members 

        private readonly IKernel _kernel;

        #endregion
        
        #region Constructor 
        
        /// <summary>
        /// SignalR NInject dependency resolver class 
        /// </summary>
        /// <param name="kernel">IKernel value</param>
        public NInjectSignalRDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        #endregion

        #region Public Methods 

        /// <summary>
        /// Get service based on a type
        /// </summary>
        /// <param name="serviceType">ServiceType value</param>
        /// <returns>Object value</returns>
        public override object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType) ?? base.GetService(serviceType);
        }

        /// <summary>
        /// Get services based on a type
        /// </summary>
        /// <param name="serviceType">ServiceType value</param>
        /// <returns>IRequest object</returns>
        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType).Concat(base.GetServices(serviceType));
        }  
        
        #endregion
    }
}