[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebApplication1.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebApplication1.App_Start.NinjectWebCommon), "Stop")]

namespace WebApplication1.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using System.Web.Http;
    using Ninject.Web.WebApi;
    using Ninject.Web.WebApi.FilterBindingSyntax;
    using System.Web.Http.Filters;
    using BlalApi.Authentication;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
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

                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

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
            //kernel
            //  .Bind(typeof(DCContext))
            //  .ToSelf().InRequestScope();

            //kernel.Bind<IUserService>().To<UserService>();
            //kernel.Bind<IRoleService>().To<RoleService>();
            //kernel.Bind<IBranchSerivce>().To<BranchService>();
            //kernel.Bind<ICharityService>().To<CharityService>();
            //kernel.Bind<IPurposeService>().To<PurposeService>();
            //kernel.Bind<IMethodService>().To<MethodService>();
            //kernel.Bind<IEnvelopeService>().To<EnvelopeService>();
            //kernel.Bind<IPersonService>().To<PersonService>();
            //kernel.Bind<IDonationService>().To<DonationService>();
            //kernel.Bind<IDonorTokenService>().To<DonorTokenService>();
            //kernel.Bind<IPaymentMethodInfoService>().To<PaymentMethodInfoService>();
            //kernel.Bind<IStripePaymentService>().To<StripePaymentService>();
            //kernel.Bind<IGoCardLessPaymentService>().To<GoCardLessPaymentService>();
            //kernel.Bind<IPaypalPayementService>().To<PaypalPayementService>();
            //kernel.Bind<ICentralOfficeService>().To<CentralOfficeService>();
            //kernel.Bind<IUserPreferenceService>().To<UserPreferenceService>();
            //kernel.Bind<IBraintreePayementService>().To<BraintreePayementService>();
            //kernel.Bind<IBranchAppService>().To<BranchAppService>();
            //kernel.Bind<ICharityAppService>().To<CharityAppService>();
            //kernel.Bind<IAppDataAccessibilityService>().To<AppDataAccessibilityService>();
            //kernel.Bind<ICentralOfficeAppService>().To<CentralOfficeAppService>();
            //kernel.Bind<IAppService>().To<AppService>();

            kernel.BindHttpFilter<CustomAuthentication>(FilterScope.Action)
               .WhenControllerHas<CustomAuthenticationAttribute>();

            //kernel.BindHttpFilter<CustomAuthenticationIntegration>(FilterScope.Action)
            //   .WhenControllerHas<CustomAuthenticationIntegrationAttribute>();

            //kernel.BindHttpFilter<CustomAuthenticationForApp>(FilterScope.Action)
            //  .WhenControllerHas<CustomAuthenticationForAppAttribute>();
            //kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            ////kernel.Bind(typeof(IUnitOfWork)).To(typeof(UnitOfWork)).InRequestScope();

            //kernel.Bind<IForgotPasswordService>().To<ForgotPasswordService>();
        }
    }
}