﻿using CommonLayer.Models;
using MassTransit;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RepositoryLayer.Interfaces
{
    public interface IUserRepository
    {
        public UserEntity UserRegister(RegisterModel register);
        public string UserLogin(LoginModel login);

        public List<UserEntity> GetAllUsers();

        public UserEntity UpdateUser(long id, RegisterModel updateproperties);
        public UserEntity GetByName(string UserName);

        public UserEntity GetUserById(long id);

        // for new user to create
        public UserEntity NewUserUpdate(RegisterModel userinfo);

        public string DeleteUser(long id);

        public List<UserEntity> GetPersonByAlphabet(string name);

        public Task<string> ForgotPassword(string emailTo, IBus bus);

        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel);


    }
}
