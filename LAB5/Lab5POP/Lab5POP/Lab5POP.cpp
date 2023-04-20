#include<iostream>
#include"omp.h"

using namespace std;

const int rows = 2000;
const int cols = 2000;
long long min_row_sum = LLONG_MAX;
const int count_of_threads = 4;

int arr[rows][cols];

void init_arr();
long long part_sum(int);
int part_min_sum(int);

int main() {
	
	init_arr();

	omp_set_nested(1);
	double t1 = omp_get_wtime();

	#pragma omp parallel sections
	{
		#pragma omp section
		{
			printf("min1 = %lld, index1 = %d \n", min_row_sum, part_min_sum(count_of_threads));
		}

		#pragma omp section
		{
			printf("sum1 = %lld \n", part_sum(count_of_threads));
		}
	}
	double t2 = omp_get_wtime();

	cout << "Total time - " << t2 - t1 << " seconds\n";
	return 0;
}

void init_arr() {
	for (int i = 0; i < rows; i++)
	{
		for (int j = 0; j < cols; j++)
		{
			arr[i][j] = i + j;
		}
	}
}

long long part_sum(int num_threads) {
	long long sum = 0;
	double t1 = omp_get_wtime();

	#pragma omp parallel for reduction(+:sum) num_threads(num_threads)
	for (int i = 0; i < rows; i++)
	{
		for (int j = 0; j < cols; j++)
		{
			sum += arr[i][j];
		}
	}


	double t2 = omp_get_wtime();

	cout << "sum " << num_threads << " threads worked - " << t2 - t1 << " seconds\n";

	return sum;
}

int part_min_sum(int num_threads) {
	min_row_sum = LLONG_MAX;
	long long row_sum = 0;
	int index = -1;
	double t1 = omp_get_wtime();

	#pragma omp parallel for reduction(+:row_sum) num_threads(num_threads)
	for (int i = 0; i < rows; i++)
	{
		for (int j = 0; j < cols; j++)
		{
			row_sum += arr[i][j];
		}
		if (min_row_sum > row_sum)
		{


#pragma omp critical
			if (min_row_sum > row_sum)
			{
				min_row_sum = row_sum;
				index = i;
			}
		}

	}


	double t2 = omp_get_wtime();

	cout << "min " << num_threads << " threads worked - " << t2 - t1 << " seconds\n";

	return index;
}

