using System;
using System.Collections.Generic;
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
                var model = new VMMachine() { Name = vm.Members["Name"].Value.ToString(), Status = vm.Members["State"].Value.ToString() };

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
        public void Edit(string VmName, int tariff)
        {
            var gb = 1;
            switch (tariff)
            {
                case 2:
                    gb = 4;
                    break;
                case 3:
                    gb = 8;
                    break;
            }
            var threads = 1;
            switch (tariff) {
                case 2:
                    threads = 2;
                    break;
                case 3:
                    threads = 4;
                    break;
            }

            PowerShell ps = PowerShell.Create();
            ps.Commands.AddScript("Stop-VM -Name " + VmName + " -Passthru | Set-VM -ProcessorCount "+threads+ " -MemoryStartupBytes "+gb+"GB -Passthru");
            ps.Invoke();
            ps.Dispose();
        }
        public void Create(int tariff,string vmName)
        {
            var ram = 1;
            switch (tariff)
            {
                case 2:
                    ram = 4;
                    break;
                case 3:
                    ram = 8;
                    break;
            }
            var hdd = 8;
            switch (tariff)
            {
                case 2:
                    hdd = 16;
                    break;
                case 3:
                    hdd = 32;
                    break;
            }
            var threads = 1;
            switch (tariff)
            {
                case 2:
                    threads = 2;
                    break;
                case 3:
                    threads = 4;
                    break;
            }
            PowerShell ps = PowerShell.Create();
            ps.Commands.AddScript($"New-VM -Name " + vmName+ " -MemoryStartupBytes " + ram+"GB -NewVHDPath d:/vhd/"+vmName+".vhdx -NewVHDSizeBytes "+hdd+ "GB | Set-VM -ProcessorCount " + threads );
            ps.Invoke();
            ps.Dispose();
        }
    }
}
