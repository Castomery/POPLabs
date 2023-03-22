public class Calculator extends Thread implements Comparable<Calculator> {
    private Integer _id;
    private Long _step;
    public Integer _workTime;

    public Integer getWorkTime(){
        return _workTime;
    }
    private  Boolean _canStop;

    public Calculator(Integer id, Long step, Integer workTime){
        _id = id;
        _step = step;
        _workTime = workTime;
        _canStop = false;
    }

    public void stopProcess(){
        _canStop = true;
    }

    @Override
    public void run(){
        Long sum = 0l;
        Long currentStepValue = 0l;

        do {
            sum += _step;
            currentStepValue++;
        }while (!_canStop);

        System.out.println(_id + " " + sum + " " + currentStepValue );
    }

    @Override
    public int compareTo(Calculator o) {
        return _workTime.compareTo(o._workTime);
    }
}
