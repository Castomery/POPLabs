public class Producer implements Runnable{
    private final int _maxItem;
    private final Storage _storage;

    public Producer(int maxItem, Storage storage) {
        _maxItem = maxItem;
        _storage = storage;
    }

    @Override
    public void run() {
        for(int i = 0; i< _maxItem; i++){
            try{
                _storage.full.acquire();
                _storage.access.acquire();

                _storage.addItem("item " + i);
                System.out.println("Added item " + i);

                _storage.access.release();
                _storage.empty.release();
            }catch (InterruptedException e){
                e.printStackTrace();
            }
        }
    }
}
