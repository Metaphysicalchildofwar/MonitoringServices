﻿namespace MonitoringServices.Models
{
    /// <summary>
    /// Модель для аккаунта, с которого запущена служба
    /// </summary>
    public class AccountsModel
    {
        /// <summary>
        /// Аккаунт
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Наименование службы
        /// </summary>
        public string ServiceName { get; set; }
    }
}
