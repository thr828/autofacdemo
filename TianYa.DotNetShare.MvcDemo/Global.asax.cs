using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Reflection;
using System.IO;

using Autofac;
using Autofac.Integration.Mvc;
using TianYa.DotNetShare.Model;

namespace TianYa.DotNetShare.MvcDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutofacRegister(); //Autofac依赖注入
        }

        /// <summary>
        /// Autofac依赖注入
        /// </summary>
        private void AutofacRegister()
        {
            var builder = new ContainerBuilder();
            //注册MVC控制器（注册所有到控制器，控制器注入，就是需要在控制器的构造函数中接收对象）
            //PropertiesAutowired：允许属性注入
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            //一次性注册所有实现了IDependency接口的类
            Type baseType = typeof(IDependency);
            Assembly[] assemblies =
                Directory.GetFiles(AppDomain.CurrentDomain.RelativeSearchPath, "*.dll").Select(Assembly.LoadFrom).ToArray();
            builder.RegisterAssemblyTypes(assemblies)
                   .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
                   .AsSelf().AsImplementedInterfaces()
                   .PropertiesAutowired().InstancePerLifetimeScope();

            //设置依赖解析器
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
