using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab3POP
{
    internal class Producer
    {
        private int _maxItem = 0;
        private Storage _storage;
        public Producer(int maxItem, Storage storage)
        {
            _maxItem = maxItem;
            _storage = storage;
        }

        public void PutItem()
        {
            for (int i = 0; i < _maxItem; i++)
            {
                _storage.full.WaitOne();
                _storage.access.WaitOne();

                _storage.AddItem("item " + i);
                Console.WriteLine("Added item " + i);

                _storage.access.Release();
                _storage.empty.Release();

            }
        }
    }
}

