using System;
using System.ServiceProcess;
using System.Windows;

namespace MonitoringServices.Services
{
    internal class ActionsForServices
    {
        public void StartService(string p)
        {
            try
            {
                ServiceController service = new ServiceController(p);

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

        public void StopService(string p)
        {
            try
            {
                ServiceController service = new ServiceController((string)p);

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
