using HumanArc.Compliance.Shared.Entities;


namespace HumanArc.Compliance.Shared.Interfaces
{
    public interface IUserService
    {
        User GetUserByUserName(string user);
    }
}
