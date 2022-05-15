﻿using MonitoringServices.Commands;
using MonitoringServices.Models;
using MonitoringServices.Services;
using MonitoringServices.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MonitoringServices.ViewModels
{
    /// <summary>
    /// Класс для ViewModel
    /// </summary>
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

        /// <summary>
        /// Выбранная служба
        /// </summary>
        public ServiceModel SelectedService
        {
            get => _selectedService;
            set => Set(ref _selectedService, value);
        }

        /// <summary>
        /// Останавливает службу
        /// </summary>
        public ICommand StopServiceCommand { get; }
        private bool CanStopServiceCommandExecute(object nameService) => true;
        private void OnStopServiceCommandExecuted(object nameService)
        {
            new Task(() => Actions.StopService((string)nameService)).Start();
        }

        /// <summary>
        /// Запускает службу
        /// </summary>
        public ICommand StartServiceCommand { get; }
        private bool CanStartServiceCommandExecute(object nameService) => true;
        private void OnStartServiceCommandExecuted(object nameService)
        {
            new Task(() => Actions.StartService((string)nameService)).Start();
        }

        /// <summary>
        /// Запускает мониторинг за службами
        /// </summary>
        private void StartMonitoringServices()
        {
            try
            {
                var services = Actions.GetServices();

                Services.AddRange(services);
                SelectedService = Services.FirstOrDefault();

                while (true)
                {
                    services = Actions.GetServices();

                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        foreach (var service in services)
                        {
                            if (Services.Any(x => x.DisplayName == service.DisplayName))
                            {
                                Services.FirstOrDefault(x => x.DisplayName == service.DisplayName).Status = service.Status;
                            }
                            else
                            {
                                Services.Add(service);
                            }
                        }
                    }));
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"В результате работы произошла ошибка: {ex.Message}");
            }
        }
    }
}
