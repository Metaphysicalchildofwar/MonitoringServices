using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MonitoringServices.Services
{
    /// <summary>
    /// Частичный класс для расширений ObservableCollections
    /// </summary>
    internal static class ObservableCollectionExtensions
    {
        /// <summary>
        /// Добавляет несколько записей в ObservableCollection
        /// </summary>
        /// <param name="observableCollection">Текущая коллекция</param>
        /// <param name="list">Список элементов для добавления</param>
        public static void AddRange<T>(this ObservableCollection<T> observableCollection, ICollection<T> list)
        {
            foreach (T item in list)
            {
                observableCollection.Add(item);
            }
        }
    }
}
