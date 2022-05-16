﻿using MonitoringServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Windows;

namespace MonitoringServices.Services
{
    /// <summary>
    /// Класс для действий над службами
    /// </summary>
    internal class ActionsForServices
    {
        /// <summary>
        /// Запускает службу
        /// </summary>
        /// <param name="nameService">Наименование службы</param>
        public void StartService(string nameService, ServiceModel selectedService)
        {
            try
            {
                ServiceController service = new ServiceController(nameService);

                if (service.Status != ServiceControllerStatus.Running)
                {
                    service.Start();
                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            selectedService.Status = ServiceControllerStatus.Running.ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }));
                    MessageBox.Show($"Служба '{nameService}' запущена!");
                }
                else
                {
                    MessageBox.Show($"Служба '{nameService}' уже запущена!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При запуске службы '{nameService}' произошла ошибка: {ex.Message}");
            };
        }

        /// <summary>
        /// Останавливает службу
        /// </summary>
        /// <param name="">Наименование службы</param>
        public void StopService(string nameService, ServiceModel selectedService)
        {
            try
            {
                ServiceController service = new ServiceController(nameService);

                if (service.Status != ServiceControllerStatus.Stopped)
                {
                    service.Stop();
                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        try
                        {
                            selectedService.Status = ServiceControllerStatus.Stopped.ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }));
                    MessageBox.Show($"Служба '{nameService}' остановлена!");
                }
                else
                {
                    MessageBox.Show($"Служба '{nameService}' уже остановлена!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При остановке службы '{nameService}' произошла ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Получает коллекцию служб
        /// </summary>
        /// <returns>Коллекция служб</returns>
        public ICollection<ServiceModel> GetServices()
        {
            var userNames = GetUserNameFromService();
            return ServiceController.GetServices().Select(x => new ServiceModel
            {
                DisplayName = x.DisplayName,
                Name = x.ServiceName,
                Account = userNames.FirstOrDefault(x => x.ServiceName == x.ServiceName).UserName,
                Status = x.Status.ToString()
            }).ToList();
        }

        /// <summary>
        /// Получает коллекцию аккаунтов, с которых была запущена служба
        /// </summary>
        /// <returns>Коллекция</returns>
        private ICollection<UserNameSeviceModel> GetUserNameFromService()
        {
            var userNames = new List<UserNameSeviceModel>();
            SelectQuery sQuery = new SelectQuery(string.Format("select name, startname from Win32_Service"));
            using (ManagementObjectSearcher mgmtSearcher = new ManagementObjectSearcher(sQuery))
            {
                foreach (ManagementObject service in mgmtSearcher.Get())
                {
                    userNames.Add(new UserNameSeviceModel { UserName = service["startname"]?.ToString(), ServiceName = service["Name"]?.ToString() });
                }
            }
            return userNames;
        }
    }
}