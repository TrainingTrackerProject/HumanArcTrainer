using Autofac;
using HumanArc.Compliance.Data;
using HumanArc.Compliance.Data.Interfaces;
using HumanArc.Compliance.Data.Repositories;
using System;

namespace HumanArc.Compliance.Web
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                    .Where(t => t.Name.EndsWith("Repository"))
                    .AsImplementedInterfaces()
                    .WithParameter("context", new ComplianceContext())
                    .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(RepositoryBase<>))
                    .As(typeof(IRepository<>));
        }
    }
}
