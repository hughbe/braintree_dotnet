#pragma warning disable 1591

using System;
using System.Collections.Generic;

namespace Braintree
{
    public class ResourceCollection<T> : System.Collections.IEnumerable where T : class
    {
        public delegate ResourceCollection<T> PagingDelegate();

        public Int32 ApproximateCount { get; protected set; }
        protected List<T> Items;
        private PagingDelegate NextPage;
        public T FirstItem {
            get {
                if (Items.Count > 0)
                {
                    return Items[0];
                }
                else
                {
                    return null;
                }
            }
        }

        public ResourceCollection(List<T> items, int totalItems, PagingDelegate nextPage)
        {
            Items = items;
            ApproximateCount = totalItems;
            NextPage = nextPage;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            ResourceCollection<T> page = this;
            while (page.Items.Count > 0)
            {
                foreach(T item in page.Items)
                {
                    yield return item;
                }
                page = page.GetNextPage();
            }
        }

        protected virtual ResourceCollection<T> GetNextPage()
        {
            return NextPage();
        }
    }
}