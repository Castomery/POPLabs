with Ada.Text_IO; use Ada.Text_IO;
with Ada.Numerics.Discrete_Random;

procedure Main is

   threads_counts : Integer := 3;
   can_stop : array(1..threads_counts) of Boolean := (others => False);
   pragma Atomic (can_stop);

   task type calculator is
      entry Init(stepin : Long_Long_Integer; idin : Integer);
   end calculator;

   task body calculator is
      steps_count  : Long_Long_Integer := 0;
      sum  : Long_Long_Integer := 0;

      step : Long_Long_Integer;
      id : Integer;
   begin
      accept Init(stepin : in Long_Long_Integer; idin : in Integer) do
            step := stepin;
            id := idin;
      end Init;
      loop
         steps_count := steps_count + 1;
         exit when can_stop(id);
      end loop;
      sum := steps_count * step;
      Put_Line ("Id: " & Id'Img & " | steps: " & steps_count'Img & " | sum: " & sum'Img);
   end calculator;

   task type thread_breaker is
      entry Init(timein : Duration; idin : Integer);
   end thread_breaker;

   task body thread_breaker is
      time : Duration;
      id :Integer;
   begin
      accept Init(timein : in Duration; idin : in Integer) do
            time := timein;
            id := idin;
      end Init;

      delay time;
      can_stop(id) := True;
   end thread_breaker;

   durations: array (1 .. threads_counts) of Duration := (2.0, 5.0, 10.0);
   steps: array (1..threads_counts) of Long_Long_Integer := (4, 1, 5);
   calculators: array (1..threads_counts) of calculator;
   thread_breakers: array (1..threads_counts) of thread_breaker;

begin
   for I in calculators'Range loop
      calculators(I).Init(steps(I), i);
      thread_breakers(I).Init(durations(I), i);
   end loop;

end Main;
