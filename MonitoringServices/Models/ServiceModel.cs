using MonitoringServices.ViewModels.Base;
using System.Collections.Generic;
using System.ServiceProcess;

namespace MonitoringServices.Models
{
    /// <summary>
    /// Класс модели для службы
    /// </summary>
    internal class ServiceModel : ViewModelBase
    {
        private readonly ICollection<string> _stopStatuses = new string[] { ServiceControllerStatus.Stopped.ToString(), ServiceControllerStatus.StopPending.ToString(), ServiceControllerStatus.PausePending.ToString(), ServiceControllerStatus.Paused.ToString() };
        
        private string _name;
        private string _displayName;
        private string _status;
        private string _account;
        private bool _stopEnable;
        private bool _startEnable;

        /// <summary>
        /// Наименование службы
        /// </summary>
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        /// <summary>
        /// Наименование службы
        /// </summary>
        public string DisplayName
        {
            get => _displayName;
            set => Set(ref _displayName, value);
        }

        /// <summary>
        /// Статус службы
        /// </summary>
        public string Status
        {
            get => _status;
            set
            {
                Set(ref _status, value);

                StopEnable = !_stopStatuses.Contains(_status);
                StartEnable = _stopStatuses.Contains(_status);
            }
        }

        /// <summary>
        /// Пользователь, с которого запущена служба
        /// </summary>
        public string Account
        {
            get => _account;
            set => Set(ref _account, value);
        }

        /// <summary>
        /// Признак возможности остановить службу
        /// </summary>
        public bool StopEnable
        {
            get => _stopEnable;
            set => Set(ref _stopEnable, value);
        }

        /// <summary>
        /// Признак возможности запустить службу
        /// </summary>
        public bool StartEnable
        {
            get => _startEnable;
            set => Set(ref _startEnable, value);
        }
    }
}
