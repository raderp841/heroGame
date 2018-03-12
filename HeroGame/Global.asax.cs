using HeroGame.DAL;
using Ninject;
using Ninject.Web.Common.WebHost;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HeroGame
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            kernel.Bind<IHeroDAL>().To<HeroDAL>().WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<IInventoryDAL>().To<InventoryDAL>().WithConstructorArgument("connectionString", connectionString);
            kernel.Bind<IUserInfoDAL>().To<UserInfoDAL>().WithConstructorArgument("connectionString", connectionString);

            return kernel;
        }
    }
}
