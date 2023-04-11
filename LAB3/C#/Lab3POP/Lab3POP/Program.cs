using System;
using System.Threading;

namespace Lab3POP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int size = 2;
            int itemCount = 10;
            int countOfConsumers = 3;
            int countOfProducers = 4;
            Storage storage = new Storage(size);

            int itemAmountToTake = itemCount;
            int itemForOneConsumer = itemCount / countOfConsumers;
            for (int i = 0; i < countOfConsumers; i++)
            {
                int amountToTake = i == (countOfConsumers - 1) ? itemAmountToTake : itemForOneConsumer;
                itemAmountToTake -= amountToTake;
                Consumer consumer = new Consumer(amountToTake, storage);
                new Thread(consumer.TakeItem).Start();
            }

            int itemAmountToAdd = itemCount;
            int itemForOneProducer = itemCount / countOfProducers;
            for (int i = 0; i < countOfProducers; i++)
            {
                int amountToAdd = i == (countOfProducers - 1) ? itemAmountToAdd : itemForOneProducer;
                itemAmountToAdd -= amountToAdd;
                Producer producer = new Producer(amountToAdd, storage);
                new Thread(producer.PutItem).Start();
            }
        }
    }
}
