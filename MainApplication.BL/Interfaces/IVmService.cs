using MainApplication.BL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.BL.Interfaces
{
    public interface IVmService
    {
        public IReadOnlyCollection<VM> GetUserVm(string email);
        public IReadOnlyCollection<VM> GetAllVMs();
        public Task AddVm(string UserName, string VmName,int tariff);
        public IReadOnlyCollection<VM> FreeVM();

    }
}
