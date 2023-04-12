import java.util.concurrent.Semaphore;

public class Main {
    public static void main(String[] args) {
        int count_of_philosophers = 5;
        Semaphore[] forks = new Semaphore[count_of_philosophers];
        for (int i = 0; i < forks.length; i++)
        {
            forks[i] = new Semaphore(1);
        }

        int count_of_loops = 1;
        int thinkingTime = 1000;
        int eatingTime = 1000;

        for (int i = 0; i < count_of_philosophers; i++)
        {
            Philosopher philosopher;
            if (i == count_of_philosophers-1)
            {
                philosopher = new Philosopher(i,
                        forks[i % (count_of_philosophers - 1)],
                        forks[i],
                        count_of_loops,
                        thinkingTime,
                        eatingTime);
            }
            else
            {
                philosopher = new Philosopher(i,
                        forks[i],
                        forks[i + 1],
                        count_of_loops,
                        thinkingTime,
                        eatingTime);
            }
            new Thread(philosopher).start();
        }
    }
}