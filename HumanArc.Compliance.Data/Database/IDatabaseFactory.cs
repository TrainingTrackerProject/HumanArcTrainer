namespace HumanArc.Compliance.Data.Database
{
    public interface IDatabaseFactory
    {
        ComplianceContext Get();
    }
}
