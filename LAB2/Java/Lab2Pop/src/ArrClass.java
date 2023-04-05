import java.util.Random;

public class ArrClass {
    private final int _dim;
    private final int _threadNum;
    public final int[] _arr;
    private int _threadCount = 0;
    private int _minIndex = 0;

    public ArrClass(int dim, int threadNum) {
        _dim = dim;
        _arr = new int[dim];
        _threadNum = threadNum;
        for(int i = 0; i < dim; i++){
            _arr[i] = i;
        }
        Random random = new Random();
        _arr[random.nextInt(dim)] *= -1;
    }

    public int findMinIndex(int startIndex, int finishIndex){
        int min = Integer.MAX_VALUE;
        int index = 0;
        for(int i = startIndex; i < finishIndex; i++){
            if (min > _arr[i])
            {
                min = _arr[i];
                index = i;
            }
        }
        return index;
    }

    public void CommonFindMin(){
        _minIndex = findMinIndex(0,_arr.length);
        printMin();
    }

    private void printMin(){
        System.out.println(_minIndex + " " + _arr[_minIndex]);
    }

    synchronized private void getMin() {
        while (getThreadCount()< _threadNum){
            try {
                wait();
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
        printMin();
    }

    synchronized public void SetMinIndex(int minIndex){
        if (_arr[_minIndex] > _arr[minIndex]){
            _minIndex = minIndex;
        }
    }


    synchronized public void incThreadCount(){
        _threadCount++;
        notify();
    }

    private int getThreadCount() {
        return _threadCount;
    }

    public void parallelFindMin(){
        ThreadMin[] threadMins = new ThreadMin[_threadNum];
        int len = _arr.length / _threadNum;

        for (int i = 0; i < _threadNum; i++)
        {
            int startIndex = len * i;
            int endIndex = (i == _threadNum - 1) ? _arr.length : len * (i + 1);
            threadMins[i] = new ThreadMin(startIndex,endIndex, this);
            threadMins[i].start();
        }
        getMin();
    }
}
