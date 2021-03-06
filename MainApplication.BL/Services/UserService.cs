using MainApplication.BL.Entities;
using MainApplication.BL.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace MainApplication.BL
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        private readonly IEmailService _emailService;
        public UserService(ApplicationContext context,IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public string Register(string email, string password)
        {
            var dbUser = _context.Users.FirstOrDefault(x => x.Email == email);
            if (dbUser != null)
            {
                return null;
            }
            int code = new Random(DateTime.Now.Millisecond).Next(1000, 9999);
            
            var newUser = new User()
            {
                Email = email,
                Code = code,
                IsConfirmed = false,
                Password = password,
                Role = Role.User
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        
            _emailService.Send(code.ToString(),email);
            var token = Login(email, password);
            return token;
        }
        public string Login(string email, string password)
        {
            var dbUser = _context.Users.FirstOrDefault(x => x.Password == password && x.Email == email);
            if (dbUser != null)
            {
                var claims = GetClaims(dbUser);
                var now = DateTime.UtcNow;
                var payload = new JwtPayload ();
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: claims.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)

                   );
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                return encodedJwt;
            }
            return null;
        }
        public ClaimsIdentity GetClaims(User user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
        public bool ConfirmEmail(string email, int code)
        {
            var dbUser = _context.Users.FirstOrDefault(x => x.Email == email && x.Code == code);
            if (dbUser != null)
            {
                dbUser.IsConfirmed = true;
                _context.SaveChanges();
                return true;
            }
            return false;

        }
       
    }
}
