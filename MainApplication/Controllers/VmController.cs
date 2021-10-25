using MainApplication.API.ViewModels;
using MainApplication.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MainApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VmController : ControllerBase
    {
        private readonly IVmService _vmService;
        public VmController(IVmService service)
        {
            _vmService = service;
        }



        [HttpPost("TurnOn")]
        [Authorize]
        public async Task<IActionResult> TurnOnAsync([FromBody] VmActionRequest action)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();


            var request = "{" + "'vmName'" + ":'" + action.VmName + "'}";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:44332/api/home/TurnOn", content);
            }
            return Ok();
        }
        [HttpPost("TurnOff")]
        [Authorize]
        public async Task<IActionResult> TurnOff([FromBody] VmActionRequest action)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();


            var request = "{" + "'vmName'" + ":'" + action.VmName + "'}";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:44332/api/home/TurnOff", content);
            }
            return Ok();
        }
        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            //достаём токен из header запроса
            var token = HttpContext.Request.Headers["Authorization"].ToString();

            //создаем клиент
            using (HttpClient client = new HttpClient())
            {
                //добавляем переданный токен в клиент
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //получаем все существующие ВМ из второго сервиса
                var response = await client.GetAsync("https://localhost:44332/api/home/all");
                //парсим строку ответа
                var responseString = await response.Content.ReadAsStringAsync();
                //десереализуем в обьект
                var deser = JsonSerializer.Deserialize<IReadOnlyCollection<VmMachineViewModel>>(responseString);
                var freeVms = _vmService.FreeVM();
                List<VmMachineViewModel> sorted = new List<VmMachineViewModel>();
               foreach(var item in deser)
                {
                    if (!freeVms.Select(x=>x.Name).Contains(item.name))
                    {
                        sorted.Add(item);
                    }
                }
                var result= sorted.Distinct();
                
                return Ok(result);
            }
        }
        [HttpPost("AddVm")]
        [Authorize]
        public async Task<IActionResult> AddVm([FromBody] AddUserVmRequest request)
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

            var str = "{" + "'vmName'" + ":'" + request.VmName + "'," + "'tariff':" + request.Tariff + "}";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var content = new StringContent(str, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:44332/api/home/edit", content);
            }
            //декодирование токена из запроса
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);

            //получение поля ИМЯ из токена

            var name = claims.Identity.Name;

            //добавление ВМ юзеру
            _vmService.AddVm(name, request.VmName, request.Tariff);

            return Ok();
        }

        [HttpGet("uservm")]
        [Authorize]
        public async Task<IActionResult> GetByName()
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
            //декодирование токена из запроса
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);

            //получение поля ИМЯ из токена
            var name = claims.Identity.Name;

            //создание клиента
            using (HttpClient client = new HttpClient())
            {
                //добавление токена из запроса в клиент
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //запрос и получение всех существующих ВМ в системе

                var response = await client.GetAsync("https://localhost:44332/api/home/all");

                //парсинг ответа в строку
                var responseString = await response.Content.ReadAsStringAsync();

                //десереализация ответа
                var allVms = JsonSerializer.Deserialize<IReadOnlyCollection<VmMachineViewModel>>(responseString);

                //получение ВМ пользователя в БД
                var uservm = _vmService.GetUserVm(name);

                //Отбор ВМ только ЮЗЕРА
                //Второе обьяснение*
                //Сравнение записей ВМ юзера в базе и всех существующих ВМ во втором сервисе
                var result = uservm.Join(allVms,
                        f => f.Name,
                        s => s.name,
                        (f, s) => new VmMachineViewModel
                        {
                            name = f.Name,
                            status = s.status,
                            id = f.Id,
                            tariff = f.Tariff.ToString()
                        }
                    );

                return Ok(result);
            }
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Add([FromBody] AddUserVmRequest action)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

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


            var claims = handler.ValidateToken(token, validations, out var tokenSecure);

            //получение поля ИМЯ из токена

            var name = claims.Identity.Name;

            string vmName = "vm" + new Random(DateTime.Now.Millisecond).Next(1000, 9999).ToString();

            var request = "{" + "'tariff'" + ":" + action.Tariff + "," +
               "'vmName'" + ":'" + vmName + "'" + "}";
            _vmService.AddVm(name, vmName, action.Tariff);
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:44332/api/home/create", content);
            }
            return Ok();
        }
    }
}
