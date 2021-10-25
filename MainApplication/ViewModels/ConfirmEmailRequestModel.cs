using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApplication.API.ViewModels
{
    public class ConfirmEmailRequestModel
    {
        public string Email { get; set; }
        public int Code { get; set; }
    }
}
