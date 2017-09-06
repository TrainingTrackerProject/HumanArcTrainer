using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.Mvc;
using AutoMapper;
using HumanArc.Compliance.Data;
using HumanArc.Compliance.Data.Database;
using HumanArc.Compliance.Service.Implementation;
using HumanArc.Compliance.Shared.Interfaces;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HumanArc.Compliance.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ComplianceContext>());
            using (var context = new ComplianceContext())
            {
                //context.Database.Initialize(true);
            }

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingConfig());
            });

            var mapper = mapperConfiguration.CreateMapper();

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterInstance(mapper).As<IMapper>();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerLifetimeScope();
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterType<TrainingService>().As<ITrainingService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<QuestionService>().As<IQuestionService>();

            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}