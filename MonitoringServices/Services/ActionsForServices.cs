using System;
using System.ServiceProcess;
using System.Windows;

namespace MonitoringServices.Services
{
    /// <summary>
    /// 
    /// </summary>
    internal class ActionsForServices
    {
        /// <summary>
        /// Запускает службу
        /// </summary>
        /// <param name="nameService">Наименование службы</param>
        public void StartService(string nameService)
        {
            try
            {
                ServiceController service = new ServiceController(nameService);

                if (service.Status != ServiceControllerStatus.Running)
                {
                    service.Start();
                    MessageBox.Show("Служба была успешно запущена!");
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
        public void StopService(string nameService)
        {
            try
            {
                ServiceController service = new ServiceController(nameService);

                if (service.Status != ServiceControllerStatus.Stopped)
                {
                    service.Stop();
                    MessageBox.Show("Служба была успешно остановлена!");
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
    }
}
