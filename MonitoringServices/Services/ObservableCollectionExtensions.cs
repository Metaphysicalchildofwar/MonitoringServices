using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MonitoringServices.Services
{
    internal static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> observableCollection, ICollection<T> list)
        {
            foreach (T item in list)
            {
                observableCollection.Add(item);
            }
        }
    }
}
