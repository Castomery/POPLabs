using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab3POP
{
    internal class Storage
    {
        public Semaphore access;
        public Semaphore full;
        public Semaphore empty;

        private List<string> _storage = new List<string>();

        public Storage(int storageSize)
        {
            access = new Semaphore(1, 1);
            full = new Semaphore(storageSize, storageSize);
            empty = new Semaphore(0,storageSize);
        }

        public string GetItem()
        {
            return _storage.ElementAt(0);
        }

        public void AddItem(string item)
        {
            _storage.Add(item);
        }

        public void RemoveItem()
        {
            _storage.RemoveAt(0);
        }
    }
}
