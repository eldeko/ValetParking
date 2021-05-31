using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ValetParking.BusinessLogic.Helpers;
using ValetParking.BusinessLogic.Interfaces;
using ValetParking.BusinessLogic.MappingExtensions;
using ValetParking.Dto;
using ValetParking.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Net;

namespace ValetParking.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/user")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IPasswordRecoveryManager _passRecoveryManager;

        public UserController(IUserService userService, IPasswordRecoveryManager passRecoveryManager)
        {
            _userService = userService;
            _passRecoveryManager = passRecoveryManager;
        }
        
        [HttpGet] [AllowAnonymous]
        [ProducesResponseType(typeof(IList<UserDto>), (int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
           var users = _userService.GetAll();
           var usersDtoList = users.MapToUserDtoList();

            return Ok(usersDtoList);
        }
        
        [HttpGet("getById")] [AllowAnonymous]
        [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
        public IActionResult GetById(string userId)
        {
            var user = _userService.GetUserByEmail(userId);
            var userDto = user.MapToUserDto();
            return Ok(userDto);
        }
        
        [HttpPost] [AllowAnonymous]
        public IActionResult RegisterUser(UserRegisterEntity inputUser)
        {
            _userService.CreateUser(inputUser);

            return Ok(inputUser);
        }
     
        [HttpPost("authenticate")] [AllowAnonymous] 
        [ProducesResponseType(typeof(LoggedUserDto), (int)HttpStatusCode.OK)]
        public IActionResult Authenticate([FromBody]UserLoginDto userParam)
        {
            IActionResult response;

            try
            {
                var user = _userService.Authenticate(userParam.Email, userParam.Password);

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                var loggedUserDto = user.MapToLoggedUserDto();

                return Ok(loggedUserDto);
            }

            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }
        
        [HttpPost("forgotpassword")] [AllowAnonymous]
        public IActionResult Forgotpassword(string email)
        {
            IActionResult response;
            try
            {
                var user = _userService.IsExistingUser(email);

                if (!user)
                    return NotFound(new { message = "Email not found" });

                _passRecoveryManager.CreateRecoveryPassword(email);

                return Ok("Email sent to given address");
            }

            catch (Exception ex)
            {
                response = BadRequest(ex.Message);
            }
            return response;
        }
       
        [HttpPost("passwordreset")] [AllowAnonymous]
        public IActionResult PasswordReset(string email, string guid, string newPassword, string repeatPassword)
        {
            var user = _userService.IsExistingUser(email);

            if (!user)
                return NotFound(new { message = "Email not found" });

            _passRecoveryManager.ProcessPasswordRecovery(email, guid, newPassword, repeatPassword);

            return Ok("Password Successfully changed");
        }
    }
}