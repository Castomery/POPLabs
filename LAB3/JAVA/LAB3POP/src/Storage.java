import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.concurrent.Semaphore;

public class Storage {

    public Semaphore access;
    public Semaphore full;
    public Semaphore empty;

    private ArrayList<String> _storage = new ArrayList<>();

    public Storage(int size){
        access= new Semaphore(1);
        full = new Semaphore(size);
        empty = new Semaphore(0);
    }

    public String getItem()
    {
        return _storage.get(0);
    }

    public void addItem(String item)
    {
        _storage.add(item);
    }

    public void removeItem()
    {
        _storage.remove(0);
    }
}
