with Ada.Text_IO; use Ada.Text_IO;
with Ada.Numerics.Discrete_Random;

procedure Main is
   dim: constant Long_Long_Integer := 200000;
   thread_num : constant Long_Long_Integer := 5;
   random_index: Long_Long_Integer;
   arr: array(1..dim) of Long_Long_Integer;

   procedure randomN is
      type randRange is new Long_Long_Integer range 1..dim;
      package Rand_Int is new ada.numerics.Discrete_Random(randRange);
      use Rand_Int;
      gen: Generator;
      num: randRange;

   begin
      reset(gen);
      num:=random(gen);
      random_index:= Long_Long_Integer(num);
   end randomN;

   procedure init_Arr is

   begin
      for i in 1..dim loop
         arr(i) := i;
      end loop;

      randomN;
      arr(random_index) := arr(random_index)*(-1);
   end init_Arr;

   function find_min(start_index, end_index : in Long_Long_Integer) return Long_Long_Integer is
      min: Long_Long_Integer := dim+1;
      min_index: Long_Long_Integer := 0;

   begin
      for i in start_index..end_index loop
         if(min > arr(i)) then
            min := arr(i);
            min_index := i;
         end if;
      end loop;
      return min_index;
   end find_min;

   task type starter_thread is
      entry start(start_index, end_index : in Long_Long_Integer);
   end starter_thread;

   protected part_manager is
      procedure set_find_min(min_index: in Long_Long_Integer);
      entry get_min(min_index: out Long_Long_Integer);
   private
      task_count: Long_Long_Integer:= 0;
      min_index1: Long_Long_Integer := 1;
   end part_manager;

   protected body part_manager is
      procedure set_find_min (min_index: in Long_Long_Integer) is
      begin
         if(arr(min_index1) > arr(min_index)) then
            min_index1 := min_index;
         end if;
         task_count := task_count + 1;
      end set_find_min;

      entry get_min(min_index: out Long_Long_Integer) when task_count = thread_num is
      begin
           min_index := min_index1;
      end get_min;
   end part_manager;

   task body starter_thread is
      min_index: Long_Long_Integer := dim+1;
      start_index, end_index : Long_Long_Integer;
   begin
      accept start (start_index, end_index : in Long_Long_Integer) do
         starter_thread.start_index := start_index;
         starter_thread.end_index := end_index;
      end start;
      min_index := find_min(start_index => start_index, end_index => end_index);
      part_manager.set_find_min(min_index);
   end starter_thread;

   function parallel_min return Long_Long_Integer is
      min_index: Long_Long_Integer := 0;
      len: Long_Long_Integer := dim/thread_num;
      start_index: Long_Long_Integer;
      end_index: Long_Long_Integer;
      thread : array(1..thread_num) of starter_thread;
   begin

      for i in 1..thread_num loop
         if(i = 1) then
            start_index := 1;
         else
            start_index := len * (i-1);
         end if;

         if(i = (thread_num-1)) then
            end_index := dim;
         else
            end_index := len * (i);
         end if;
         thread(i).start(start_index,end_index);
      end loop;
      part_manager.get_min(min_index);
      return min_index;
   end parallel_min;

   result: Long_Long_Integer;
begin
   init_Arr;
   --  result:= find_min(1,dim);
   --  Put_Line("Common");
   --  Put_Line(arr(result)'img & " " & result'img);
   --  Put_Line("");
   Put_Line("Parallel");
   result:= parallel_min;
   Put_Line(arr(result)'img & " " & result'img);
end Main;
