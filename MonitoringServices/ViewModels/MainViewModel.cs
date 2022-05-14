using MonitoringServices.Commands;
using MonitoringServices.Models;
using MonitoringServices.Services;
using MonitoringServices.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MonitoringServices.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public ObservableCollection<ServiceModel> Services { get; set; } = new ObservableCollection<ServiceModel>();

        private readonly ActionsForServices Actions;
        public MainViewModel()
        {
            StartServiceCommand = new ActionCommand(OnStartServiceCommandExecuted, CanStartServiceCommandExecute);
            StopServiceCommand = new ActionCommand(OnStopServiceCommandExecuted, CanStopServiceCommandExecute);

            Actions = new ActionsForServices();

            new Task(() => StartMonitoringServices()).Start();
        }
        private ServiceModel _selectedService;
        public ServiceModel SelectedService
        {
            get => _selectedService;
            set => Set(ref _selectedService, value);
        }

        public ICommand StopServiceCommand { get; }
        private bool CanStopServiceCommandExecute(object p) => true;
        private void OnStopServiceCommandExecuted(object p)
        {
            new Task(() => Actions.StopService((string)p)).Start();
        }

        public ICommand StartServiceCommand { get; }
        private bool CanStartServiceCommandExecute(object p) => true;
        private void OnStartServiceCommandExecuted(object p)
        {
            new Task(() => Actions.StartService((string)p)).Start();
        }

        private void StartMonitoringServices()
        {
            try
            {
                ServiceController[] services = ServiceController.GetServices();

                Services.AddRange(services.Select(x => new ServiceModel
                {
                    DisplayName = x.DisplayName,
                    Name = x.ServiceName,
                    Account = x.MachineName,
                    Status = x.Status.ToString()
                }).ToList());
                SelectedService = Services.FirstOrDefault();

                while (true)
                {
                    Thread.Sleep(1000);

                    services = ServiceController.GetServices();

                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        foreach (ServiceController s in services)
                        {
                            if (Services.Any(x => x.DisplayName == s.DisplayName))
                            {
                                ServiceModel needService = Services.FirstOrDefault(x => x.DisplayName == s.DisplayName);
                                needService.DisplayName = s.DisplayName;
                                needService.Status = s.Status.ToString();
                                needService.Name = s.ServiceName;
                                needService.Account = s.MachineName;
                            }
                            else
                            {
                                Services.Add(new ServiceModel
                                {
                                    Status = s.Status.ToString(),
                                    Account = s.MachineName,
                                    Name = s.ServiceName,
                                    DisplayName = s.DisplayName
                                });
                            }
                        }
                    });

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В результате работы произошла ошибка: {ex.Message}");
            }
        }
    }
}
