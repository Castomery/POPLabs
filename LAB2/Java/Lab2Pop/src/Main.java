import java.util.Random;

public class Main {
    public static void main(String[] args) throws InterruptedException {
        int dim = 1_000_000_000;
        int threadNum = 5;
        int[] arr = new int[dim];

        for(int i = 0; i < dim; i++){
            arr[i] = i;
        }
        Random random = new Random();
        arr[random.nextInt(dim)] *= -1;

        ArrClass arrClass = new ArrClass(dim, threadNum, arr);

        System.out.println("Common");
        int result = arrClass.findMinIndex(0,dim);
        System.out.println("Min = " + arr[result] + " index = " + result);
        Thread.sleep(2000);
        System.out.println("Parallel");
        result = arrClass.parallelFindMin();
        System.out.println("Min = " + arr[result] + " index = " + result);
    }
}