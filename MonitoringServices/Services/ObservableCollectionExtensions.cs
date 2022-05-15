using MonitoringServices.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MonitoringServices.Services
{
    /// <summary>
    /// Класс для расширений коллецкии ObservableCollections
    /// </summary>
    internal static class ObservableCollectionExtensions
    {
        /// <summary>
        /// Добавляет список в коллекцию
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

        public static void RemoveRange<T>(this ObservableCollection<T> observableCollection, ICollection<T> list)
        {
            foreach (T item in list)
            {
                observableCollection.Remove(item);
            }
        }
    }
}
