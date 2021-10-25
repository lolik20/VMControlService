using MainApplication.BL.Entities;
using MainApplication.BL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainApplication.BL.Services
{
    public class VmService : IVmService
    {
        private readonly ApplicationContext _context;
        public VmService(ApplicationContext context)
        {
            _context = context;
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
        public void AddVm(string UserName, string VmName, int Tariff)
        {
            //метод добавления ВМ юзеру
            //получение юзера из бд с ВМ
            var user = _context.Users.Include(x => x.VMs).FirstOrDefault(x => x.Email == UserName);


            //преобразование int в enum тип
            Tariff tariff = (Tariff)Enum.Parse(typeof(Tariff), Tariff.ToString());
            if (user != null)
            {
                //получение ВМ по имени
                var vm = _context.VMs.FirstOrDefault(x => x.Name == VmName);
                //Если вм есть в базе, замена тарифа и присваивание ВМ юзеру
                if (vm != null)
                {
                    vm.Tariff = tariff;
                    user.VMs.Add(vm);
                    _context.SaveChanges();
                }
                //Если вм нет в базе, создание присваивание ВМ юзеру

                else
                {
                    var newVm = new VM { Name = VmName, Tariff = tariff };
                    _context.VMs.Add(newVm);
                    user.VMs.Add(newVm);
                    _context.SaveChanges();
                }
            }
        }
    }

}
