using System;
using HumanArc.Compliance.Shared.Entities;
using HumanArc.Compliance.Shared.Interfaces;
using HumanArc.Compliance.Data.Repositories;

namespace HumanArc.Compliance.Service.Implementation
{
    public class UserService : IUserService
    { 
        private readonly UserRepository _userRepo;
        public UserService(UserRepository repository)
        {
            _userRepo = repository;
        }

        public User GetUserByUserName(string userName)
        {
           return  _userRepo.GetUserByUserName(userName);
        }
    }
}
