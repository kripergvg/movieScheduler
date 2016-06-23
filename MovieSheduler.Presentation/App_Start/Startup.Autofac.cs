using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using MovieSheduler.Application.Infrastructure;
using MovieSheduler.Application.SheduleRecord;
using MovieSheduler.Domain.Infrastructure;
using MovieSheduler.EntityFramework;
using MovieSheduler.EntityFramework.Infrastructure;
using MovieSheduler.EntityFramework.Repositories.SheduleRecord;
using MovieSheduler.Presentation.Core.Messager;

namespace MovieSheduler.Presentation
{
    public partial class Startup
    {
        public IContainer ConfigureAutofac()
        {
            var builder = new ContainerBuilder();

            builder.Register(c => new HttpContextWrapper(HttpContext.Current)).As<HttpContextBase>().InstancePerRequest();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterAssemblyTypes(typeof(SheduleRecordService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(SheduleRecordRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.RegisterType<MovieShedulerContext>().InstancePerLifetimeScope()
                .As<DbContext>()
                .As<MovieShedulerContext>();

            builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>();
            builder.RegisterType<ValidationDictionary>().As<IValidationDictionary>();
            builder.RegisterType<Notifier>().As<INotifier>().InstancePerRequest();
            builder.Register(b => new NotifierFilterAttribute(b.Resolve<INotifier>())).AsActionFilterFor<Controller>().InstancePerRequest();
            builder.RegisterFilterProvider();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            return container;
        }
    }
}