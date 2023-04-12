import java.util.concurrent.Semaphore;

public class Philosopher implements Runnable{

    private final int _id;
    private final Semaphore _left_fork;
    private final Semaphore _right_fork;
    private final int _count_of_loops;
    private final int _time_to_eat;
    private final int _time_to_think;

    public Philosopher(int id, Semaphore left_fork, Semaphore right_fork, int count_of_loops, int time_to_eat, int time_to_think)
    {
        _id = id;
        _left_fork = left_fork;
        _right_fork = right_fork;
        _count_of_loops = count_of_loops;
        _time_to_eat = time_to_eat;
        _time_to_think = time_to_think;
    }
    @Override
    public void run() {
        for (int i = 0; i < _count_of_loops; i++)
        {
            try{
                System.out.println("Philosopher " + _id + " thinking " + i + " time");
                Thread.sleep(_time_to_think);

                _left_fork.acquire();
                System.out.println("Philosopher " + _id + " take left fork");
                _right_fork.acquire();
                System.out.println("Philosopher " + _id + " take right fork");

                System.out.println("Philosopher " + _id + " eating " + i + " time");
                Thread.sleep(_time_to_eat);

                _right_fork.release();
                System.out.println("Philosopher " + _id + " put right fork");
                _left_fork.release();
                System.out.println("Philosopher " + _id + " put left fork");
            }catch (InterruptedException e){
                e.printStackTrace();
            }

        }

    }
}
