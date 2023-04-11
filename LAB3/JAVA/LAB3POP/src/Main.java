public class Main {
    public static void main(String[] args) {
        int storageSize = 3;
        int itemCount = 10;
        int countOfConsumers = 3;
        int countOfProducers = 4;
        Storage storage = new Storage(storageSize);

        int itemAmountToTake = itemCount;
        int itemForOneConsumer = itemCount / countOfConsumers;
        for (int i = 0; i < countOfConsumers; i++)
        {
            int amountToTake = i == (countOfConsumers - 1) ? itemAmountToTake : itemForOneConsumer;
            itemAmountToTake -= amountToTake;
            Consumer consumer = new Consumer(amountToTake, storage);
            new Thread(consumer).start();
        }

        int itemAmountToAdd = itemCount;
        int itemForOneProducer = itemCount / countOfProducers;
        for (int i = 0; i < countOfProducers; i++)
        {
            int amountToAdd = i == (countOfProducers - 1) ? itemAmountToAdd : itemForOneProducer;
            itemAmountToAdd -= amountToAdd;
            Producer producer = new Producer(amountToAdd, storage);
            new Thread(producer).start();
        }
    }
}