using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Repositorys;
using System.Linq;

namespace BusinessLayer.Controllers
{
    public class UserController
    {
        private DataAccesLayer.UnitOfWork unitOfWork;

        public UserController(DataAccesLayer.MyContext context)
        {
             unitOfWork = new DataAccesLayer.UnitOfWork(context);
        }

        //register user
        public void RegisterNewUser(string username, string name, string password, string mail, int phoneNbr)
        {
            DataAccesLayer.Models.User newUser = new DataAccesLayer.Models.User()
            {
                Username = username,
                Name = name,
                Password = password,
                PhoneNumber = phoneNbr,
                Email = mail
            };

            unitOfWork.UserRepository.Add(newUser);
            unitOfWork.SaveChanges();
        }

        //Get by user ID
        public DataAccesLayer.Models.User GetByUserName(string username)
        {
            return unitOfWork.UserRepository.FirstOrDefault(u => u.Username == username);
        } 

        //login
        public bool LogIn(DataAccesLayer.Models.User user, string passWord)
        {
            if (user.Password == passWord)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckIfUsernameExists(string userName)
        {
            if (unitOfWork.UserRepository.FirstOrDefault(u => u.Username == userName) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
