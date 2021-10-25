using System;
using System.Collections.Generic;
using System.Text;

namespace MainApplication.BL.Interfaces
{
   public interface IUserService
    {
        public string Register(string email, string password);
        public string Login(string email, string password);
        public bool ConfirmEmail(string email, int code);
    }
}
