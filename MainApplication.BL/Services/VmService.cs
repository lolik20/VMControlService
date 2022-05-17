using MainApplication.BL.Entities;
using MainApplication.BL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.BL.Services
{
    public class VmService : IVmService
    {
        private readonly ApplicationContext _context;
        private readonly IMemoryCache _cache;
        public VmService(ApplicationContext context,IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }
        public IReadOnlyCollection<VM> FreeVM()
        {
            var vms = _context.VMs
                .Where(x => x.User != null);

            return vms.ToList();
        }
        public IReadOnlyCollection<VM> GetUserVm(string email)
        {
            //получение вм юзера
            var VMs = _context.Users
                .Include(x => x.VMs)
                .FirstOrDefault(x => x.Email == email)
                .VMs;


            return VMs;
        }
        public IReadOnlyCollection<VM> GetAllVMs()
        {
            //получение всех вм из базы
            var VMs = _context.VMs
                .Where(x => x.User == null);

            return VMs.ToList();
        }
        public async Task AddVm(string UserName, string VmName, int Tariff)
        {
            //метод добавления ВМ юзеру
            //получение юзера из бд с ВМ
            var user = await _context.Users.Include(x => x.VMs).FirstOrDefaultAsync(x => x.Email == UserName);

            if (user != null)
            {
                lock (user)
                {
                    var cache = _cache.Get(user.Id);
                    if(cache != null)
                    {
                        throw new Exception("Подождите создание VM");
                    }
                }
                Tariff tariff = await _context.Tariffs.FirstOrDefaultAsync(x => x.Id == Tariff);

                //получение ВМ по имени
                var vm = await _context.VMs.FirstOrDefaultAsync(x => x.Name == VmName);
                //Если вм есть в базе, замена тарифа и присваивание ВМ юзеру
                if (vm != null)
                {
                    vm.Tariff = tariff;
                    user.VMs.Add(vm);
                    _context.SaveChanges();
                }
                //Если вм нет в базе, создание и присваивание ВМ юзеру

                else
                {
                    var newVm = new VM { Name = VmName, Tariff = tariff };
                    _context.VMs.Add(newVm);
                    user.VMs.Add(newVm);
                    _context.SaveChanges();
                }
                _cache.Set(user.Id, user,TimeSpan.FromMinutes(1));
            }
        }
    }

}
