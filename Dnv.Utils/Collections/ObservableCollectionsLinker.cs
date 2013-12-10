using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Dnv.Utils.Collections
{
    /// <summary>
    /// Класс связывает две коллекции типа ObservableCollection между собой так, 
    /// что изменения (добавление и удаление элементов) в исходной коллекции автоматически отображаются на 
    /// назначаемой коллекции. Пока отслеживается только добавление и удаление. Перемещение не отслеживается.
    /// </summary>
    /// <typeparam name="TSourceItemType">Тип исходной коллекции.</typeparam>
    /// <typeparam name="TDestItemType">Тип коллекции, в которой нужно отобразить изменения исходной коллекции</typeparam>
    public class ObservableCollectionsLinker<TSourceItemType, TDestItemType>: IDisposable
        where TDestItemType: class
    {
        readonly ObservableCollection<TSourceItemType> _sourceCollection;
        readonly ObservableCollection<TDestItemType> _destCollection;

        readonly Func<TSourceItemType, TDestItemType> _constructDestItem;

        /// <summary>
        /// Конструктор. После создания экземпляра изменения в коллекциях связываются автоматически.
        /// </summary>
        /// <param name="sourceCollection">Исходная коллекция, изменения в которой нужно отслеживать.</param>
        /// <param name="destCollection">Коллекция, в которой нужно отобразить изменения исходной.</param>
        /// <param name="constructDestItem">Функция, которая конструирует новый элемент типа DestItemType на 
        /// основе SourceItemType. Может возвращать null, тогда объект не будет добавляться.</param>
        public ObservableCollectionsLinker(ObservableCollection<TSourceItemType> sourceCollection,
            ObservableCollection<TDestItemType> destCollection, Func<TSourceItemType, TDestItemType> constructDestItem)
        {
            _sourceCollection = sourceCollection;
            _destCollection = destCollection;

            _constructDestItem = constructDestItem;

            _sourceCollection.CollectionChanged += SourceCollectionChanged;
        }


        void SourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                ApplyAddChanges(e);
            else if (e.Action == NotifyCollectionChangedAction.Remove)
                ApplyRemoveChanges(e);
            else if (e.Action == NotifyCollectionChangedAction.Reset)
                _destCollection.Clear();
        }
        

        private void ApplyRemoveChanges(NotifyCollectionChangedEventArgs e)
        {
            // работоспособность проверена теоретически.
            int index = e.OldStartingIndex;
            foreach (var sourceItem in e.OldItems)
            {
                _destCollection.RemoveAt(index);
            }
        }

        private void ApplyAddChanges(NotifyCollectionChangedEventArgs e)
        {
            foreach (var sourceItem in e.NewItems)
            {
                var destItem = _constructDestItem((TSourceItemType)sourceItem);
                if (destItem != null)
                    _destCollection.Add(destItem);
            }
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            _sourceCollection.CollectionChanged -= SourceCollectionChanged;
        }

        #endregion
    }
}
