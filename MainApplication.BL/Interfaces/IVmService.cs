using MainApplication.BL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainApplication.BL.Interfaces
{
    public interface IVmService
    {
        public IReadOnlyCollection<VM> GetUserVm(string email);
        public IReadOnlyCollection<VM> GetAllVMs();
        public void AddVm(string UserName, string VmName,int tariff);
        public IReadOnlyCollection<VM> FreeVM();

    }
}
