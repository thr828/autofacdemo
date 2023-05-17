using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using Autofac;
using Autofac.Integration.Web;
using TianYa.DotNetShare.Model;

namespace TianYa.DotNetShare.WebDemo
{
    public class Global : System.Web.HttpApplication, IContainerProviderAccessor
    {
        /// <summary>
        /// 依赖注入ContainerProvider
        /// </summary>
        static IContainerProvider _containerProvider;

        /// <summary>
        /// 实现IContainerProviderAccessor接口
        /// </summary>
        public IContainerProvider ContainerProvider
        {
            get
            {
                return _containerProvider;
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            AutofacRegister(); //Autofac依赖注入
        }

        /// <summary>
        /// Autofac依赖注入
        /// </summary>
        private void AutofacRegister()
        {
            var builder = new ContainerBuilder();

            //一次性注册所有实现了IDependency接口的类
            Type baseType = typeof(IDependency);
            Assembly[] assemblies = 
                Directory.GetFiles(AppDomain.CurrentDomain.RelativeSearchPath, "*.dll").Select(Assembly.LoadFrom).ToArray();
            builder.RegisterAssemblyTypes(assemblies)
                   .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
                   .AsSelf().AsImplementedInterfaces()
                   .PropertiesAutowired().InstancePerLifetimeScope();

            //设置依赖解析器
            _containerProvider = new ContainerProvider(builder.Build());
        }
    }
}