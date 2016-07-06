using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using ModuleManager.Domain;
using ModuleManager.Domain.Interfaces;
using ModuleManager.Domain.Repositories;
using ModuleManager.Web.App_Start;
using ModuleManager.Web.Controllers.Api;
using ModuleManager.Web.Controllers.Api.Interfaces;
using Ninject;
using Ninject.Web.Common;
using WebActivatorEx;
using ModuleManager.BusinessLogic.Services;
using ModuleManager.BusinessLogic.Interfaces.Services;
using ModuleManager.UserDAL.Interfaces;
using ModuleManager.UserDAL.Repositories;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace ModuleManager.Web.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // Domain entity repositories:          
            kernel.Bind<IGenericRepository<Competentie>>().To<GenericRepository<Competentie>>();
            kernel.Bind<IGenericRepository<Fase>>().To<GenericRepository<Fase>>();
            kernel.Bind<IGenericRepository<Leerlijn>>().To<GenericRepository<Leerlijn>>();
            kernel.Bind<IGenericRepository<Module>>().To<GenericRepository<Module>>();
            kernel.Bind<IGenericRepository<Tag>>().To<GenericRepository<Tag>>();
            kernel.Bind<IGenericRepository<Blok>>().To<GenericRepository<Blok>>();
            kernel.Bind<IGenericRepository<Niveau>>().To<GenericRepository<Niveau>>();
            kernel.Bind<IGenericRepository<Schooljaar>>().To<GenericRepository<Schooljaar>>();
            kernel.Bind<IGenericRepository<Status>>().To<GenericRepository<Status>>();
            kernel.Bind<IGenericRepository<FaseType>>().To<GenericRepository<FaseType>>();
            kernel.Bind<IGenericRepository<Module>>().To<GenericRepository<Module>>(); 

            // UnitOfWork session for repositories to use:
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            // Domain entity API controllers:
            kernel.Bind<IGenericApiController<Competentie>>().To<CompetentieController>();
            kernel.Bind<IGenericApiController<Fase>>().To<FaseController>();
            kernel.Bind<IGenericApiController<Leerlijn>>().To<LeerlijnController>();
            kernel.Bind<IGenericApiController<Tag>>().To<TagController>();
            kernel.Bind<IModuleApiController>().To<ModuleController>();

            // User entity repositories:
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<ISysteemRolRepository>().To<SysteemRolRepository>();

            // Filter-, Sorter- and Export-services:
            kernel.Bind<IFilterSorterService<Module>>().To<ModuleFilterSorterService>();
            kernel.Bind<IExporterService<Module>>().To<ModuleExporterService>();
            kernel.Bind<IExporterService<Competentie>>().To<CompetentieExporterService>();
            kernel.Bind<IExporterService<Leerlijn>>().To<LeerlijnExporterService>();
            kernel.Bind<IExporterService<FaseType>>().To<LessenTabelExporterService>();

        }
    }
}
