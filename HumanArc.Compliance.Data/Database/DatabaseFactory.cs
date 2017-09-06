namespace HumanArc.Compliance.Data.Database
{
    public class DatabaseFactory : IDatabaseFactory
    {
        public ComplianceContext Get()
        {
            return new ComplianceContext();
        }
    }
}
