using MonitoringServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    MessageBox.Show("Служба успешно запущена!");
                }
                else
                {
                    MessageBox.Show("Служба уже запущена!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При запуске службы произошла ошибка: {ex.Message}");
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
                    MessageBox.Show("Служба успешно остановлена!");
                }
                else
                {
                    MessageBox.Show("Служба уже остановлена!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При остановке службы произошла ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Получает коллекцию служб
        /// </summary>
        /// <returns>Коллекция служб</returns>
        public ICollection<ServiceModel> GetServices()
        {
            return ServiceController.GetServices().Select(x => new ServiceModel
            {
                DisplayName = x.DisplayName,
                Name = x.ServiceName,
                Account = x.MachineName,
                Status = x.Status.ToString()
            }).ToList();
        }
    }
}
