using Autofac;
using System;
using System.Reflection;

namespace Csp.Utilities.Wpf.Mvvm
{
    public static class IoC
    {
        private static ContainerBuilder containerBuilder = new ContainerBuilder();

        public static void Register(Action<ContainerBuilder> register) => register(containerBuilder);

        public static ContainerBuilder RegisterAssembly(this ContainerBuilder @this, Assembly assembly, params string[] @namespace)
        {
            var b = @this.RegisterAssemblyTypes(assembly);

            if (@namespace.Length > 0)
            {
                b.Where(t => EndsWithIn(t.Namespace, @namespace));
            }
            b.AsImplementedInterfaces()
             .AsSelf();

            return @this;
        }

        public static void Build(Autofac.Builder.ContainerBuildOptions containerBuildOptions = Autofac.Builder.ContainerBuildOptions.None)
        {
            Container = containerBuilder.Build(containerBuildOptions);
        }

        public static IContainer Container { get; private set; }

        public static T Resolve<T>() => Container.Resolve<T>();

        public static ILifetimeScope BeginLifetimeScope() => Container.BeginLifetimeScope();

        private static bool EndsWithIn(string value, params string[] list)
        {
            foreach (var item in list)
            {
                if (value.EndsWith(item))
                    return true;
            }

            return false;
        }

        public static ContainerBuilder RegisterSingleton<T>(this ContainerBuilder @this)
        {
            @this.RegisterType<T>().SingleInstance();
            return @this;
        }
    }
}
