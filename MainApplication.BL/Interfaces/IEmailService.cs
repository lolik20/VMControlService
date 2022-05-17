using System;
using System.Collections.Generic;
using System.Text;

namespace MainApplication.BL.Interfaces
{
    public interface IEmailService
    {
        public void Send(string message,string email);
    }
}
