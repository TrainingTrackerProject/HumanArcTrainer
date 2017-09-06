using System.Data.Entity;


namespace HumanArc.Compliance.Data
{
    public class ComplianceInitializer : DropCreateDatabaseIfModelChanges<ComplianceContext>
    {
        protected override void Seed(ComplianceContext context)
        {
        }
    }
}
