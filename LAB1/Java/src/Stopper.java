import java.lang.reflect.Array;
import java.util.Arrays;

public class Stopper implements Runnable {
    private Calculator[] _calculators;

    public Stopper(Calculator[] calculators){
        _calculators = calculators;
        Arrays.sort(_calculators);
    }

    @Override
    public void run() {
        long currentWaitedTime = 0;

        for (int i = 0; i<_calculators.length;i++){
            long waitingTime = _calculators[i].getWorkTime() - currentWaitedTime;
            try {
                Thread.sleep(waitingTime);
                currentWaitedTime += waitingTime;
                _calculators[i].stopProcess();
            } catch (InterruptedException e) {
                throw new RuntimeException(e);
            }
        }
    }
}
