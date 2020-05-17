using System;
using System.Collections.Generic;
using System.Text;

namespace task
{
    class Matrix
    {
        public double[,] matrix;
        public int rowsMatrix;

        public double[,] Create(int count)
        {

            if (count != 0)
            {
                Random random = new Random();
                double[,] myMatrix = new double[count, count];

                for (int i = 0; i < count; ++i)
                {
                    for (int j = 0; j < count; ++j)
                    {
                        myMatrix[i, j] = random.Next(1, 10);
                    }
                }
                return myMatrix;
            }
            return null;
        }

        public double[,] Revers(double[,] myMatrix, int count)
        {
            double[,] reversMatrix = new double[count, count];
            double buff;

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (i == j)
                    { reversMatrix[i, j] = 1; }
                    else
                    { reversMatrix[i, j] = 0; }
                }
            }

            for (int j = 0; j < count; j++)
            {
                for (int i = 0; i < count; i++)
                {
                    if (i == j)
                    { 
                        goto k; 
                    }

                    buff = myMatrix[i, j] / myMatrix[j, j];

                    for (int y = 0; y < count; y++)
                    {

                        myMatrix[i, y] = myMatrix[i, y] - myMatrix[j, y] * buff;
                        reversMatrix[i, y] = reversMatrix[i, y] - reversMatrix[j, y] * buff;
                    }
                    k:;
                }
            }

            for (int j = 0; j < count; j++)
            {
                for (int i = 0; i < count; i++)
                {
                    if (i == j)
                    {
                        buff = myMatrix[i, j];

                        for (int y = 0; y < count; y++)
                        {
                            myMatrix[i, y] = myMatrix[i, y] / buff;
                            reversMatrix[i, y] = reversMatrix[i, y] / buff;
                        }


                    }
                }
            }

            return reversMatrix;
        }
    }
}
