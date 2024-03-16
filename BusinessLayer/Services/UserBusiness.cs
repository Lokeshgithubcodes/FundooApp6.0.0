using BusinessLayer.Interfaces;
using CommonLayer.Models;
using MassTransit;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository userRepo;


        public UserBusiness(IUserRepository userRepo)
        {
            this.userRepo = userRepo;

        }

        public UserEntity UserRegister(RegisterModel register)
        {
            return userRepo.UserRegister(register);
        }

        public string UserLogin(LoginModel login)
        {
            return userRepo.UserLogin(login);
        }

        

        public List<UserEntity> GetAllUsers()
        {
            return userRepo.GetAllUsers();
        }

        public UserEntity UpdateUser(long id, RegisterModel updateproperties)
        {
            return userRepo.UpdateUser(id, updateproperties);
        }

        public UserEntity GetByName(string UserName)
        {
            return userRepo.GetByName(UserName);
        }

        public UserEntity GetUserById(long id)
        {
            return userRepo.GetUserById(id);
        }


        public UserEntity NewUserUpdate(RegisterModel userinfo)
        {
            return userRepo.NewUserUpdate(userinfo);
        }

        public string DeleteUser(long id)
        {
            return userRepo.DeleteUser(id);
        }

        public List<UserEntity> GetPersonByAlphabet(string name)
        {
            return userRepo.GetPersonByAlphabet(name);

        }

        public async Task<string> ForgotPassword(string emailTo, IBus bus)
        {
            return await userRepo.ForgotPassword(emailTo, bus);
        }

        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel)
        {
            return userRepo.ResetPassword(Email, resetPasswordModel);
        }

        public TokenModel LoginMethod(LoginModel model)
        {
            return userRepo.LoginMethod(model);
        }
    }
}
