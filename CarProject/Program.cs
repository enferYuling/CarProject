using Autofac;
using CarProject.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CarProject
{
    internal static class Program
    {
        public static IContainer Container { get; set; }
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            var builder = new ContainerBuilder();

            // Register SqlSugarClient as a singleton
            builder.Register(c => SqlSugarConfig.GetInstance()).As<SqlSugarClient>().SingleInstance();

            // Register forms
             builder.RegisterType<LoginIndex.LoginIndex>().AsSelf().InstancePerDependency();
           //  builder.RegisterType<HomeIndex.HomeIndex>().AsSelf().InstancePerDependency();
            Container = builder.Build();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
             Application.Run(Container.Resolve<LoginIndex.LoginIndex>());
           // Application.Run(Container.Resolve<HomeIndex.HomeIndex>());
        }
    }
}
