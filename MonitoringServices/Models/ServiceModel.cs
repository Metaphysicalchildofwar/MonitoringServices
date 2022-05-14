﻿using MonitoringServices.Models.Base;
using MonitoringServices.ViewModels.Base;
using System.Linq;

namespace MonitoringServices.Models
{
    internal class ServiceModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _displayName;
        public string DisplayName
        {
            get => _displayName;
            set => Set(ref _displayName, value);
        }

        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                Set(ref _status, value);

                StopEnable = !StopStatuses.Statuses.Contains(_status);
                StartEnable = StopStatuses.Statuses.Contains(_status);
            }
        }

        private string _account;
        public string Account
        {
            get => _account;
            set => Set(ref _account, value);
        }

        private bool _stopEnable;
        public bool StopEnable
        {
            get => _stopEnable;
            set => Set(ref _stopEnable, value);
        }

        private bool _startEnable;
        public bool StartEnable
        {
            get => _startEnable;
            set => Set(ref _startEnable, value);
        }
    }
}
