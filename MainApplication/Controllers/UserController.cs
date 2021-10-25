using MainApplication.API.ViewModels;
using MainApplication.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MainApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestModel request)
        {
            //метод регистрации юзера
           var response  = _userService.Register(request.Email,request.Password);
            var result = new ResultModel()
            {
                IsSuccessful = true,
                Token=response
            };
            return Ok(result);
        }
        
        [AllowAnonymous]
        [HttpPost("confirm")]
        public IActionResult Confirm([FromBody] ConfirmEmailRequestModel request)
        {
            //метод подтверждения почты
           var response= _userService.ConfirmEmail(request.Email, request.Code);
            var result = new ResultModel()
            {
                IsSuccessful = response
            };
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestModel request)
        {
            //аутентификация юзера
           var result= _userService.Login(request.Email, request.Password);
            //создание result модели
            var response = new ResultModel()
            {
                IsSuccessful = true,
                Token = result
            };
            //возврат модели с токеном
            return Ok(response);
        }   

    }
}
