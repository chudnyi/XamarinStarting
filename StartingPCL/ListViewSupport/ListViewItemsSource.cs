using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http.Headers;

namespace StartingPCL.ListViewSupport
{
    public class ListViewItemsSource<T> : IReadOnlyList<T>, INotifyCollectionChanged
    {
//        public delegate T ItemAtIndex(int index);

        public Func<int, T> ItemAtIndex { get; set; }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ListViewItemsSource(int count)
        {
            Count = count;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public int Count { get; }

        public T this[int index]
        {
            get
            {
                var item = this.ItemAtIndex(index);
                return item;
            }
        }

        public void UpdateItemAtIndex(T item, T oldItem, int index)
        {
            Log.Info($"update item at index: {index}");
            this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, oldItem, index) );
        }
    }
    
}