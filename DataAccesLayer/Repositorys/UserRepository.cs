using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Models;
using DataAccesLayer.Interface;

namespace DataAccesLayer.Repositorys
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MyContext context) : base(context)
        {
        }
    }
}
