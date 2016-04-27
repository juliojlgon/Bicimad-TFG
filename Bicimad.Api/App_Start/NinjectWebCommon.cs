using System.Linq;
using AutoMapper;
using Bicimad.Core;
using Bicimad.Services.Command;
using Bicimad.Services.Command.Interface;
using Bicimad.Services.Query;
using Bicimad.Services.Query.Interfaces;
using Bicimad.Web.DependencyResolution;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Bicimad.Api.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Bicimad.Api.App_Start.NinjectWebCommon), "Stop")]

namespace Bicimad.Api.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

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
            var profiles =
               from t in typeof(DefaultRegistry).Assembly.GetTypes()
               where typeof(Profile).IsAssignableFrom(t)
               select (Profile)Activator.CreateInstance(t);

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });
            var mapper = config.CreateMapper();
            kernel.Bind<IMapper>().ToConstant(mapper);

            kernel.Bind<IRepository>().To<EFRepository>();
            kernel.Bind<IUserCommandService>().To<UserCommandService>();
            kernel.Bind<IUserQueryService>().To<UserQueryService>();
            kernel.Bind<ISecurityQueryService>().To<SecurityQueryService>();
            kernel.Bind<IStationQueryService>().To<StationQueryService>();
            kernel.Bind<IReservationQueryService>().To<ReservationQueryService>();
            kernel.Bind<IReservationCommandService>().To<ReservationCommandService>();
            kernel.Bind<ISlotQueryService>().To<SlotQueryService>();
        }        
    }
}
