using Autofac;
using ClassicDrawingApplication.Helpers;
using ClassicDrawingApplication.Interfaces;
using ClassicDrawingApplication.Services;

namespace ClassicDrawingApplication.Config
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ClassicDrawingApplication>().As<IClassicDrawingApplication>();
            builder.RegisterType<ToolsService>().As<IToolsService>();
            builder.RegisterType<ShapeHelper>().As<IShapeHelper>();

            return builder.Build();
        }
    }
}
