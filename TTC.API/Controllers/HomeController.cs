using HyperV.API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTC.BL;

namespace TTC.API.Controllers
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IVmService _test;
        public HomeController(IVmService test)
        {
            _test = test;
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            //надо переделать валидацию токена и вынести в отдельный middleware



            //парсинг токена из запроса
             var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            //создание handler
            string secret = "mysupersecret_secretkey!123";
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            try
            {
                //валидация
                var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            //получение всех ВМ
            var response = _test.GetAll();
            return Ok(response);
        }

        [HttpPost("TurnOn")]
        public IActionResult TurnOn([FromBody] VmActionRequest request)
        {

            //надо переделать валидацию токена и вынести в отдельный middleware


            //парсинг токена из запроса
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            //создание handler
            string secret = "mysupersecret_secretkey!123";
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            try
            {
                //валидация
                var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            //выключение ВМ по VmName из запроса

            _test.TurnOn(request.VmName);
            return Ok();
        }
        [HttpPost("TurnOff")]
        public IActionResult TurnOff([FromBody] VmActionRequest request)
        {

            //надо переделать валидацию токена и вынести в отдельный middleware


            //парсинг токена из запроса
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            //создание handler
            string secret = "mysupersecret_secretkey!123";
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            try
            {
                //валидация
                var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            }
            catch(Exception)
            {
                return BadRequest();
            }
            //выключение ВМ по VmName из запроса

            _test.TurnOff(request.VmName);
            return Ok();
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] VmActionRequest request)
        {
            //парсинг токена из запроса
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            //создание handler
            string secret = "mysupersecret_secretkey!123";
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            try
            {
                //валидация
                var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            //создание vm исходя из тарифа
            _test.Create(request.Tariff,request.VmName);
            return Ok();
        }
        [HttpPost("edit")]
        public IActionResult Edit([FromBody] VmActionRequest request)
        {
            //парсинг токена из запроса
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            //создание handler
            string secret = "mysupersecret_secretkey!123";
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            try
            {
                //валидация
                var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            //изменение vm
            _test.Edit(request.VmName,request.Tariff);
            return Ok();
        }
    }
}
