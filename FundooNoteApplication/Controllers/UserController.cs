using BusinessLayer.Interfaces;
using CommonLayer.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;

        private readonly IBus bus;

        public UserController(IUserBusiness userBusiness, IBus bus)
        {
            this.userBusiness = userBusiness;
            this.bus = bus;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterModel model)
        {

            var result = userBusiness.UserRegister(model);
            if (result != null)
            {
                return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Register Successfull", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Register Not Successfull" });
            }
        }


        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel login)
        {
            var logResult = userBusiness.UserLogin(login);
            if (logResult != null)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "Login Successfull", Data = logResult });
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Login Not Successfull" });
            }

        }

        [HttpGet("Byid")]

        public IActionResult GetUserDetails(long id)
        {
            var user = userBusiness.GetUserById(id);
            if (user != null)
            {
                return Ok(new ResponseModel<UserEntity>{Success = true, Message = "User Detail found", 
                    Data = user});

            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = false, Message = "User Details not found" });
            }
        }

        [HttpGet]
        [Route("All Users")]

        public IActionResult GetAllUsers()
        {
            try
            {
                var users=userBusiness.GetAllUsers();
                if(users!= null && users.Any())
                {
                    return Ok(value: new ResponseModel<List<UserEntity>> { Success = true, Message = "Successfully Fetch User Details",
                        Data = users
                    });
                }
                else
                {
                    return BadRequest("Not able to Fetch Data");
                }

            }catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Update user By Id")]

        public IActionResult CreateorUpdateUser(long id, UserUpdateModel update)
        {
            var userinfo=userBusiness.UpdateUser(id, update);
            if (userinfo!=null)
            {
                return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Successfully update User info", Data = userinfo });
            }
            else
            {
                return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Invalid info" });
            }
        }


        [HttpGet("Name")]

        public IActionResult GetUserByName(string name)
        {
            var userDetails=userBusiness.GetByName(name);
            if (userDetails != null)
            {
                return Ok(userDetails);
            }
            else
            {
                return BadRequest("Invalid user");
            }
        }

        //[HttpPost]
        //[Route("Update/Create")]

        //public IActionResult CreateorUpdateUser(RegisterModel registerModel)
        //{
        //    var userinfo=userBusiness.NewUserUpdate(registerModel);
        //    if (userinfo!=null)
        //    {
        //        return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Successfully update User info", Data = userinfo });
        //    }
        //    else
        //    {
        //        return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Invalid info" });
        //    }
        //}

        [HttpDelete]
        [Route("delete User")]

        public IActionResult DeleteUser(long id)
        {
            var deleteUser=userBusiness.DeleteUser(id);
            if(deleteUser!=null)
            {
                return Ok(deleteUser);
            }
            else
            {
                return BadRequest("Not Deleted");
            }
        }

        [HttpGet]
        [Route("GetByFirstLetter")]

        public IActionResult GetAllUserByAlphabet(string name)
        {
            var user=userBusiness.GetPersonByAlphabet(name);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("No user found");
            }
        }


        [HttpPost]
        [Route("forgot password")]

        public IActionResult ForgotPassword(string emailTo)
        {
            try
            {
                var result = userBusiness.ForgotPassword(emailTo, bus);
                if (result != null)
                {
                    return Ok(new ResponseModel<string> { Success = true, Message = "suceesfull" });

                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "not suceesfull" });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("resetPassword")]
        public IActionResult UpdatePassword(ResetPasswordModel reset)
        {
            string Email=User.Claims.FirstOrDefault(a=>a.Type== "Email").Value;
            var res = userBusiness.ResetPassword(Email, reset);
            if (res != null)
            {
                return Ok(new { success = true, message = "ResetPassword updated Successfully" });

            }
            else
            {
                return BadRequest("Password is not Updated");
            }
        }

        [HttpPost("LoginMethod")]
        public IActionResult LoginMethod(LoginModel model)
        {
            var login = userBusiness.LoginMethod(model);
            if (login != null)
            {
                long UserId=login.Id;
                byte[] userbyte=BitConverter.GetBytes(UserId);
                HttpContext.Session.Set("UserId", userbyte);
                return Ok(new { success = true, message = "User Login Successful", Data = login });
            }
            else
            {
                return BadRequest(new { success = false, message = "User Login Unsuccessful" });
            }

        }

    }
}
