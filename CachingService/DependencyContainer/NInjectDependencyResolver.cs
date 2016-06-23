//|---------------------------------------------------------------|
//|                     CACHING SERVICE                           |
//|---------------------------------------------------------------|
//|                     Developed by Wonde Tadesse                |
//|                        Copyright ©2015 - Present              |
//|---------------------------------------------------------------|
//|                     CACHING SERVICE                           |
//|---------------------------------------------------------------|
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using Ninject.Activation;

namespace CachingService.DependencyContainer
{
    /// <summary>
    /// NInject dependency resolver class 
    /// </summary>
    public class NInjectDependencyResolver : NInjectScope, IDependencyResolver
    {
        #region Private Variable

        private readonly IKernel _kernel;

        #endregion

        #region Public Properties

        /// <summary>
        /// Get Kernel value 
        /// </summary>
        public IKernel Container
        {
            get { return _kernel; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// NInject dependency resolver class
        /// </summary>
        /// <param name="container"></param>
        public NInjectDependencyResolver(IKernel container)
            : base(container)
        {
            _kernel = container;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Begin dependency scope
        /// </summary>
        /// <returns>IDependencyScope object</returns>
        public new IDependencyScope BeginScope()
        {
            return new NInjectScope(_kernel);
        }

        #endregion
    }

}