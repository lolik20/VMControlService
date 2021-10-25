using System;
using System.Collections.Generic;
using System.Text;

namespace MainApplication.BL.Entities
{
    public class VM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public Tariff Tariff { get; set; }
        
    }
}
