﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab3POP
{
    internal class Consumer
    {
        private int _maxItem = 0;
        private Storage _storage;
        public Consumer(int maxItem, Storage storage)
        {
            _maxItem = maxItem;
            _storage = storage;
        }

        public void TakeItem()
        {
            for (int i = 0; i < _maxItem; i++)
            {
                _storage.empty.WaitOne();
                Thread.Sleep(1000);
                _storage.access.WaitOne();

                string item = _storage.GetItem();
                _storage.RemoveItem();

                _storage.full.Release();

                _storage.access.Release();

                Console.WriteLine("Took " + item);
            }
        }
    }
}
