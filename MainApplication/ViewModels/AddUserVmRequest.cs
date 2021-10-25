using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApplication.API.ViewModels
{
    public class AddUserVmRequest
    {
        public string VmName { get; set; }
        public int Tariff { get; set; }
    }
}
