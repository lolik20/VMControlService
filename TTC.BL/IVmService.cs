using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace TTC.BL
{
   public interface IVmService
    {
        public IReadOnlyCollection<VMMachine> GetAll();
        public void TurnOn(string VmName);
        public void TurnOff(string VmName);
        public void Create(int tariff,string vmName);
        public void Edit(string VmName, int tariff);

    }
}
