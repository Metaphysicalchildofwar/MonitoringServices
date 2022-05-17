namespace MonitoringServices.Models
{
    /// <summary>
    /// Модель для аккаунта, с которого запущена служба
    /// </summary>
    internal class AccountsModel
    {
        /// <summary>
        /// Аккаунт
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Наименование службы
        /// </summary>
        public string ServiceName { get; set; }
    }
}
