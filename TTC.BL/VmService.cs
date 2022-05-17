using MainApplication.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Management.Automation;
using System.Text;

namespace TTC.BL
{
    public class VMMachine
    {
        //модель виртуалки
        public string Name { get; set; }
        public string Status { get; set; }
    }
    public class VmService : IVmService
    {
        private readonly ApplicationContext _context;
        public VmService(ApplicationContext context)
        {
            _context = context;
        }
        public IReadOnlyCollection<VMMachine> GetAll()
        {
            //создание клиента powershell
            PowerShell ps = PowerShell.Create();
            //выставление доступа как АДМИН
            ps.Commands.AddScript("Set-ExecutionPolicy Unrestricted");
            //выполнение
            ps.Invoke();
            ps.Commands.Clear();
            //добавление команды
            ps.AddScript("get-vm");
            //выполнение
            ps.Invoke();
            var psResult = ps.Invoke();
            //создание пустой коллекции результата
            List<VMMachine> result = new List<VMMachine>();

            foreach (PSObject vm in psResult)
            {
                //наполнение коллекции результата по свойствам
                var model = new VMMachine()
                {
                    Name = vm.Members["Name"].Value.ToString(),
                    Status = vm.Members["State"].Value.ToString()
                };

                result.Add(model);
            }
            //закрытие клиента PowerShell
            ps.Dispose();
            return result;
        }
        public void TurnOn(string VmName)
        {
            //создание клиента 
            PowerShell ps = PowerShell.Create();
            //старт ВМ по переданному имени
            ps.Commands.AddScript("Start-VM -Name " + VmName);
            ps.Invoke();
            ps.Dispose();
        }
        public void TurnOff(string VmName)
        {
            //создание клиента
            PowerShell ps = PowerShell.Create();
            //стоп ВМ по имени
            ps.Commands.AddScript("Stop-VM -Name " + VmName + " -Force");
            ps.Invoke();
            ps.Dispose();
        }
        public void Edit(string VmName, int tariffId)
        {
            var tariffModel = _context.Tariffs.FirstOrDefault(x => x.Id == tariffId);

            PowerShell ps = PowerShell.Create();
            ps.Commands.AddScript("Stop-VM -Name " + VmName + " -Passthru | Set-VM -ProcessorCount " + tariffModel.Threads + " -MemoryStartupBytes " + tariffModel.Ram + "GB -Passthru");
            ps.Invoke();
            ps.Dispose();
        }
        public void Create(int tariffId, string vmName)
        {
            var tariffModel = _context.Tariffs.FirstOrDefault(x => x.Id == tariffId);

            PowerShell ps = PowerShell.Create();
            ps.Commands.AddScript($"New-VM -Name " + vmName + " -MemoryStartupBytes " + tariffModel.Ram + "GB -NewVHDPath d:/vhd/" + vmName + ".vhdx -NewVHDSizeBytes " + tariffModel.HDD + "GB | Set-VM -ProcessorCount " + tariffModel.Threads);
            ps.Invoke();
            ps.Dispose();
        }
    }
}
