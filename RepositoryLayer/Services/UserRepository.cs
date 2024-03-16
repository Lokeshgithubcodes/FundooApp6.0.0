using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using MassTransit;
using RepositoryLayer.Migrations;
using Microsoft.Extensions.Logging;

namespace RepositoryLayer.Services
{
    public class UserRepository:IUserRepository
        
    {
        private readonly FundooContext fundooContext;

        private readonly IConfiguration configuration;

       private readonly ILogger<UserRepository> logger;



        public UserRepository(FundooContext fundooContext, IConfiguration configuration, ILogger<UserRepository> logger)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
            this.logger = logger;
        }



        public UserEntity UserRegister(RegisterModel register)
        {
            logger.LogError("Registration Error");
            UserEntity entity = new UserEntity();
            entity.FirstName = register.FirstName;
            entity.LastName = register.LastName;
            entity.Email = register.Email;
            entity.Password = EncryptPassword(register.Password);
            fundooContext.UserTable.Add(entity);
            fundooContext.SaveChanges();
            return entity;

        }

        public static string EncryptPassword(string password)
        {
            try
            {
                byte[] encrypt_password = new byte[password.Length];
                encrypt_password = Encoding.UTF8.GetBytes(password);
                string encodedPassword = Convert.ToBase64String(encrypt_password);
                return encodedPassword;

            }
            catch (Exception ex)
            {
                return $"Encryption Failed.! {ex.Message}";
            }
        }

        public static string DecryptPassword(string encryptedPassword)
        {
            try
            {
                byte[] decrypt_password = Convert.FromBase64String(encryptedPassword);
                string originalPassword = Encoding.UTF8.GetString(decrypt_password);
                return originalPassword;
            }
            catch (Exception ex)
            {
                return $"Decryption Failed.! {ex.Message}";
            }
        }

        public string UserLogin(LoginModel login)
        {
            // Fetch user by email from the database
            var userFromDb = fundooContext.UserTable.FirstOrDefault(x => x.Email == login.Email);

            if (userFromDb != null && DecryptPassword(userFromDb.Password) == login.Password)
            {
                var token = GenerateToken(userFromDb.Email, (int)userFromDb.UserId);
                return token;
            }
            else
            {

                return null;
            }
        }

        public string GenerateToken(string email, long userId)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",email),
                new Claim("UserId",userId.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public List<UserEntity> GetAllUsers()
        {
            Trace.WriteLine("Inside getting all the Users");
            var users= new List<UserEntity>();
            users=fundooContext.UserTable.ToList();
            return users;
        }

        public UserEntity UpdateUser(long  id, RegisterModel updateproperties)
        {
            
            var existingUser = fundooContext.UserTable.Find(id);
            if (existingUser != null)
            {
                existingUser.FirstName = updateproperties.FirstName ?? existingUser.FirstName;
                existingUser.LastName= updateproperties.LastName ?? existingUser.LastName;
                existingUser.Email= updateproperties.Email ?? existingUser.Email;
                existingUser.Password=EncryptPassword(updateproperties.Password) ?? existingUser.Password;
                fundooContext.SaveChanges();
                return existingUser;
            }
            else
            {
                return null;
            }
        }

        public UserEntity GetByName(string UserName)
        {

            logger.LogError("Invalid Name");
            var UserDetails=fundooContext.UserTable.FirstOrDefault(x=>x.FirstName==UserName);
            if(UserDetails != null)
            {
                return UserDetails;
            }else
            {
                return null;
            }
        }

        public UserEntity GetUserById(long id)
        {
            UserEntity user = fundooContext.UserTable.FirstOrDefault(x => x.UserId == id);
            return user;
        }

        public UserEntity NewUserUpdate(RegisterModel userinfo)
        {
            var userDetails=fundooContext.UserTable.FirstOrDefault(x=>x.Email==userinfo.Email);
           
            if (userDetails != null)
            {
                if (userDetails.UserId != null)
                {
                    userDetails.FirstName = userinfo.FirstName;
                    userDetails.LastName = userinfo.LastName;
                    userDetails.Email = userinfo.Email;
                    userDetails.Password = userinfo.Password;
                    fundooContext.SaveChanges();
                    return userDetails;


                }
                else
                {
                    return null;
                }

            }
            else
            {
                UserEntity user = new UserEntity();
                user.FirstName = userinfo.FirstName;
                user.LastName = userinfo.LastName;
                user.Email = userinfo.Email;
                user.Password = userinfo.Password;
                fundooContext.UserTable.Add(user);
                fundooContext.SaveChanges();
                return user;
            }
        }

       public string DeleteUser(long id)
       {
            var deleteUser = fundooContext.UserTable.Find(id);
            if (deleteUser != null)
            {
                fundooContext.UserTable.Remove(deleteUser);
                fundooContext.SaveChanges();
                return "User Deleted Successfully";
            }
            else
            {
                return "Invalid User Id";
            }
       }

        public List<UserEntity> GetPersonByAlphabet(string name)
        {
            var user=fundooContext.UserTable.Where(x=>x.FirstName.StartsWith(name)).ToList();
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }



        public async Task<string> ForgotPassword(string emailTo, IBus bus)
        {
            try
            {
                if (string.IsNullOrEmpty(emailTo))
                {
                    return null;
                }
                else
                {
                    var result = fundooContext.UserTable.FirstOrDefault(x => x.Email == emailTo);
                    if (result == null)
                    {
                        return "invalid email";
                    }
                    else
                    {
                        var tkn = GenerateToken(emailTo, result.UserId);
                        Sent sent = new Sent();
                        sent.SendMessage(emailTo, tkn);

                        Uri uri = new Uri("rabbitmq://localhost/NotesEmail_Queue");
                        var endpoint = await bus.GetSendEndpoint(uri);
                        return tkn;

                    }

                }

            }
            catch (Exception e)
            {

                return e.Message;
            }

        }

        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel)
        {
            UserEntity User = fundooContext.UserTable.ToList().Find(x => x.Email == Email);
            if (User != null)
            {
                User.Password = EncryptPassword(resetPasswordModel.confirmpassword);
                //User.ChangedAt = DateTime.Now;
                fundooContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public TokenModel LoginMethod(LoginModel model)
        {
            var user = fundooContext.UserTable.FirstOrDefault(s => s.Email == model.Email);
            if (user != null)
            {
                //model.Password= EncodePassword(user.Password);
                TokenModel tk = new TokenModel();
                if (model.Password == DecryptPassword(user.Password) && model.Email == user.Email)
                {
                    tk.Email = user.Email;
                    tk.First_name = user.FirstName;
                    tk.Last_Name = user.LastName;
                    tk.Token = GenerateToken(user.Email, user.UserId);
                    tk.Id = user.UserId;
                    return tk;

                }
                return null;

            }
            return null;
        }


    }

}
