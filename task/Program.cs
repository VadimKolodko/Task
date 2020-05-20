using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace task
{
    class Program
    {
        Thread thread;
        private static int globalCountMatrix = 0;

        static void Main(string[] args)
        {
            Console.Write("\nWrite count matrix: count = ");

            int countMatrix = Int32.Parse(Console.ReadLine());

            Console.Write("\nWrite rows in matrix: n = ");

            int rowsMatrix = Int32.Parse(Console.ReadLine());

            Console.Write("\nSelect a method from the list:");
            Console.Write("\n   1 - Synchronous single-threaded;");
            Console.Write("\n   2 - Synchronous multithreaded;");
            Console.Write("\n   3 - Asynchronous single-threaded;");
            Console.Write("\n   4 - Asynchronous multithreaded;\n");

            int method = Int32.Parse(Console.ReadLine());

            switch (method)
            {
                case 1:
                    Synchronous_Single_Threaded(countMatrix, rowsMatrix);
                    break;
                case 2:
                    Synchronous_Multithreaded(countMatrix, rowsMatrix);
                    break;
                case 3:
                    Asynchronous_Single_Threaded(countMatrix, rowsMatrix);
                    Thread.Sleep(100000);
                    break;
                case 4:
                    Asynchronous_Multithreaded(countMatrix, rowsMatrix);
                    Thread.Sleep(100000);
                    break;
            }
        }

        private static void Synchronous_Single_Threaded(int countMatrix, int rowsMatrix)
        {
            if (countMatrix != 0)
            {
                DateTime now = DateTime.Now;

                Matrix matrix = new Matrix();

                for (int countCycle = 0; countCycle < countMatrix; countCycle++)
                {
                    double[,] myMatrix = matrix.Create(rowsMatrix);

                    Console.WriteLine("\nMatrix number " + (countCycle + 1).ToString() + ":");

                    for (int i = 0; i < rowsMatrix; i++)
                    {
                        for (int y = 0; y < rowsMatrix; y++)
                        {
                            Console.Write(myMatrix[i, y].ToString());
                            Console.Write(" ");
                        }

                        Console.WriteLine();
                    }

                    Console.WriteLine("\nRevers matrix number " + (countCycle + 1).ToString() + ":");

                    double[,] myMatrixRevers = matrix.Revers(myMatrix, rowsMatrix);

                    for (int i = 0; i < rowsMatrix; i++)
                    {
                        for (int y = 0; y < rowsMatrix; y++)
                        {
                            Console.Write(myMatrixRevers[i, y].ToString());
                            Console.Write(" ");
                        }

                        Console.WriteLine();
                    }
                }

                TimeSpan time = DateTime.Now - now;
                Console.WriteLine(time.ToString());
            }
        }

        private static void Synchronous_Multithreaded(int countMatrix, int rowsMatrix)
        {
            if(countMatrix != 0)
            {
                DateTime now = DateTime.Now;

                Thread[] threads = new Thread[countMatrix];

                for(int i = 0; i < countMatrix; i++)
                {
                    threads[i] = new Thread(Sync);
                    threads[i].Name = "Thread number " + (i + 1).ToString();
                    threads[i].Start(rowsMatrix);
                }
            }
        }

        private static async void Asynchronous_Single_Threaded(int countMatrix, int rowsMatrix)
        {
            await Task.Run(() => AsyncSingle(countMatrix, rowsMatrix));
        }

        private static async void Asynchronous_Multithreaded(int countMatrix, int rowsMatrix)
        {
            await Task.Run(() => AsyncCreate(countMatrix, rowsMatrix));
        }

        static void Sync(object rowsMatrix)
        {
            Matrix matrix = new Matrix();

            double[,] myMatrix = matrix.Create((int)rowsMatrix);

            Console.WriteLine("\nMatrix in thread name: " + Thread.CurrentThread.Name + ":");

            for (int i = 0; i < (int)rowsMatrix; i++)
            {
                for (int y = 0; y < (int)rowsMatrix; y++)
                {
                    Console.Write(myMatrix[i, y].ToString());
                    Console.Write(" ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("\nRevers matrix in thread name: " + Thread.CurrentThread.Name + ":");

            double[,] myMatrixRevers = matrix.Revers(myMatrix, (int)rowsMatrix);

            for (int i = 0; i < (int)rowsMatrix; i++)
            {
                for (int y = 0; y < (int)rowsMatrix; y++)
                {
                    Console.Write(myMatrixRevers[i, y].ToString());
                    Console.Write(" ");
                }

                Console.WriteLine();
            }
        }

        static void AsyncSingle(int countMatrix, int rowsMatrix)
        {
            DateTime now = DateTime.Now;

            Matrix[] matrices = new Matrix[countMatrix];

            for (int i = 0; i < countMatrix; i++)
            {
                matrices[i] = new Matrix();
            }

            for (int countCycle = 0; countCycle < countMatrix; countCycle++)
            {
                matrices[countCycle].matrix = matrices[countCycle].Create(rowsMatrix);

                Console.WriteLine("\nMatrix number " + (countCycle + 1).ToString() + ":");

                for (int i = 0; i < rowsMatrix; i++)
                {
                    for (int y = 0; y < rowsMatrix; y++)
                    {
                        Console.Write(matrices[countCycle].matrix[i, y].ToString());
                        Console.Write(" ");
                    }

                    Console.WriteLine();
                }

                Console.WriteLine("\nRevers matrix number " + (countCycle + 1).ToString() + ":");

                double[,] myMatrixRevers = matrices[countCycle].Revers(matrices[countCycle].matrix, rowsMatrix);

                for (int i = 0; i < rowsMatrix; i++)
                {
                    for (int y = 0; y < rowsMatrix; y++)
                    {
                        Console.Write(myMatrixRevers[i, y].ToString());
                        Console.Write(" ");
                    }

                    Console.WriteLine();
                }
            }

            TimeSpan time = DateTime.Now - now;
            Console.WriteLine(time.ToString());
        }

        static void AsyncCreateMatrix(Matrix[] matrix)
        {
            for (int countCycle = 0; countCycle < globalCountMatrix; countCycle++)
            {
                matrix[countCycle].matrix = matrix[countCycle].Create(matrix[countCycle].rowsMatrix);

                Console.WriteLine("\nMatrix number " + (countCycle + 1).ToString() + ":");

                for (int i = 0; i < matrix[countCycle].rowsMatrix; i++)
                {
                    for (int y = 0; y < matrix[countCycle].rowsMatrix; y++)
                    {
                        Console.Write(matrix[countCycle].matrix[i, y].ToString());
                        Console.Write(" ");
                    }

                    Console.WriteLine();
                }
            }
        }

        static void AsyncReversMatrix(Matrix[] matrix)
        {
            for (int countCycle = 0; countCycle < globalCountMatrix; countCycle++)
            {
                Console.WriteLine("\nRevers matrix number " + (countCycle + 1).ToString() + ":");

                double[,] myMatrixRevers = matrix[countCycle].Revers(matrix[countCycle].matrix, matrix[countCycle].rowsMatrix);

                for (int i = 0; i < matrix[countCycle].rowsMatrix; i++)
                {
                    for (int y = 0; y < matrix[countCycle].rowsMatrix; y++)
                    {
                        Console.Write(myMatrixRevers[i, y].ToString());
                        Console.Write(" ");
                    }

                    Console.WriteLine();
                }
            }
        }

        static void AsyncCreate(int countMatrix, int rowsMatrix)
        {
            DateTime now = DateTime.Now;

            Thread[] threads = new Thread[countMatrix];
            Matrix[] matrices = new Matrix[countMatrix];
            globalCountMatrix = countMatrix;

            for (int i = 0; i < countMatrix; i++)
            {
                matrices[i] = new Matrix();
                matrices[i].rowsMatrix = rowsMatrix;
            }

            for (int i = 0; i < countMatrix; i++)
            {
                threads[i] = new Thread(() => AsyncCreateMatrix(matrices));
                threads[i].Name = "Thread number " + (i + 1).ToString();
                threads[i].Start();
            }

            Thread.Sleep(1000);

            for (int i = 0; i < countMatrix; i++)
            {
                threads[i] = new Thread(() => AsyncReversMatrix(matrices));
                threads[i].Name = "Thread number " + (i + 10).ToString();
                threads[i].Start();
            }

            TimeSpan time = DateTime.Now - now;
            Console.WriteLine(time.ToString());
        }
    }
}
