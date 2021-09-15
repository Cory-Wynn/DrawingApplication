using Autofac;
using ClassicDrawingApplication.Config;
using ClassicDrawingApplication.Interfaces;

namespace ClassicDrawingApplication
{
    class Program
    {
        private static IContainer Container;

        static void Main(string[] args)
        {
            Container = ContainerConfig.Configure();

            using (var scope = Container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IClassicDrawingApplication>();
                app.Run();
            }
        }
    }
}
