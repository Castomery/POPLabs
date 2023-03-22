public class Main {
    public static void main(String[] args) {

        Calculator[]calculators = {new Calculator(1,3l,2000),
                                   new Calculator(2,2l,5000),
                                   new Calculator(4,5l,10000)};
        
        Thread[]threads = new Thread[calculators.length];
        Stopper stopper = new Stopper(calculators);

        for (int i = 0; i < threads.length; i++){
            threads[i] = new Thread(calculators[i]);
            threads[i].start();
        }
        new Thread(stopper).start();
    }
}