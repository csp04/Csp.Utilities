using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Csp.Utilities.Wpf.Mvvm.TestApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var asm = Assembly.GetExecutingAssembly();

            IoC.Register(builder =>
           {
               builder.RegisterAssembly(asm, "ViewModels")
                      .RegisterAssemblyTypes(asm)
                        .Where(t => t.IsClosedTypeOf(typeof(WindowView<>)) || 
                                    t.IsClosedTypeOf(typeof(PageView<>)) || 
                                    t.IsClosedTypeOf(typeof(UserControlView<>)));
           });

            IoC.Build();

            using(var scope = IoC.BeginLifetimeScope())
            {
                var window = scope.Resolve<MainWindow>();
                window.ShowDialog();
            }
        }
    }
}
