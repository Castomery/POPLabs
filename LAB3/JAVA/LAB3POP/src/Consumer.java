public class Consumer implements Runnable{
    private final int _maxItem;
    private final Storage _storage;

    public Consumer(int maxItem, Storage storage) {
        _maxItem = maxItem;
        _storage = storage;
    }

    @Override
    public void run() {
        for (int i = 0; i < _maxItem; i++) {
            String item;
            try {
                _storage.empty.acquire();
                Thread.sleep(1000);
                _storage.access.acquire();

                item = _storage.getItem();
                _storage.removeItem();
                System.out.println("Took " + item);

                _storage.access.release();
                _storage.full.release();

            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }
}
