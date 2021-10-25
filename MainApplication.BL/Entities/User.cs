using System;
using System.Collections.Generic;
using System.Text;

namespace MainApplication.BL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Code { get; set; }
        public bool IsConfirmed { get; set; }
        public Role Role { get; set; }
        public List<VM> VMs { get; set; }
    }
}
