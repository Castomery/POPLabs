with Ada.Text_IO; use Ada.Text_IO;
with GNAT.Semaphores; use GNAT.Semaphores;

procedure Main is
   count_of_philosophers : constant Integer := 5;
   count_of_repeats: constant Integer := 5;
   time_to_think: constant Duration := 1.5;
   time_to_eat: constant Duration:= 1.5;

   forks: array(1..count_of_philosophers) of Counting_Semaphore(1,Default_Ceiling);

   task type Phylosopher is
      entry Start(cur_id, cur_left_fork_id, cur_right_fork_id : Integer);
   end Phylosopher;

   task body Phylosopher is
      Id : Integer;
      left_fork_id : Integer;
      right_fork_id : Integer;
   begin
      accept Start (cur_id,cur_left_fork_id,cur_right_fork_id : Integer) do
         Id:= cur_id;
         left_fork_id:= cur_left_fork_id;
         right_fork_id := cur_right_fork_id;
      end Start;

      for I in 1..count_of_repeats loop
         Put_Line("Phylosopher " & Id'Img & " thinking " & I'Img & " time");
         delay time_to_think;

         forks(left_fork_id).Seize;
         Put_Line("Phylosopher " & Id'Img & " took left fork");
         forks(right_fork_id).Seize;
         Put_Line("Phylosopher " & Id'Img & " took right fork");

         Put_Line("Phylosopher " & Id'Img & " eating " & I'Img & " time");

         forks(right_fork_id).Release;
         Put_Line("Phylosopher " & Id'Img & " put right fork");
         forks(left_fork_id).Release;
         Put_Line("Phylosopher " & Id'Img & " put left fork");
      end loop;
   end Phylosopher;

   phylosophers : array(1..count_of_philosophers) of Phylosopher;

begin
   for I in 1..count_of_philosophers loop
      if(I = count_of_philosophers) then
         phylosophers(I).Start(I,I rem count_of_philosophers + 1,I);
      else
         phylosophers(I).Start(I,I,I + 1);
      end if;
   end loop;

end Main;
