using HumanArc.Compliance.Shared.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading;

namespace HumanArc.Compliance.Data
{
    public class ComplianceContext : DbContext
    {
        public ComplianceContext() 
            : base ("name=ComplianceDB")
        {

        }

        public DbSet<Training> Trainings { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ADGroup> Groups { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is EntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = Thread.CurrentPrincipal.Identity.Name;

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((EntityBase)entity.Entity).DateCreated = DateTime.Now;
                    ((EntityBase)entity.Entity).CreatedBy = currentUsername;
                }

                if (entity.State == EntityState.Modified)
                {

                    var dbValues =  entity.GetDatabaseValues();
                    ((EntityBase) entity.Entity).CreatedBy = dbValues.GetValue<String>("CreatedBy");
                    ((EntityBase)entity.Entity).DateCreated = dbValues.GetValue<DateTime>("DateCreated");
                }


                ((EntityBase)entity.Entity).DateModified = DateTime.Now;
                ((EntityBase)entity.Entity).ModifiedBy = currentUsername;
            }

            return base.SaveChanges();
        }
    }
}
