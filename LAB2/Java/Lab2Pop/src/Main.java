public class Main {
    public static void main(String[] args) throws InterruptedException {
        int dim = 1_000_000_000;
        int threadNum = 5;
        ArrClass arrClass = new ArrClass(dim, threadNum);

        System.out.println("Common");
        arrClass.CommonFindMin();
        Thread.sleep(2000);
        System.out.println("Parallel");
        arrClass.parallelFindMin();
    }
}