using System;
using System.IO;

namespace LibMas
{
    public static class MatrixHelper
    {
        public static void InitMatrix(
            out int[,] matrix,
            int rows,
            int columns,
            int minValue,
            int maxValue)
        {
            matrix = new int[rows, columns];
            Random rnd = new Random();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = rnd.Next(minValue, maxValue + 1);
                }
            }
        }
        public static void SaveMatrix(string fileName, int[,] matrix)
        {
            using (StreamWriter file = new StreamWriter(fileName))
            {
                int rows = matrix.GetLength(0);
                int columns = matrix.GetLength(1);
                file.WriteLine(rows);
                file.WriteLine(columns);
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        file.Write(matrix[i, j] + " ");
                    }

                    file.WriteLine();
                }
            }
        }
        public static int[,] OpenMatrix(string fileName)
        {
            using (StreamReader file = new StreamReader(fileName))
            {
                int rows = Convert.ToInt32(file.ReadLine());
                int columns = Convert.ToInt32(file.ReadLine());

                int[,] matrix = new int[rows, columns];

                for (int i = 0; i < rows; i++)
                {
                    string line = file.ReadLine();

                    string[] values = line.Split(
                        new char[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries);

                    for (int j = 0; j < columns; j++)
                    {
                        matrix[i, j] = Convert.ToInt32(values[j]);
                    }
                }

                return matrix;
            }
        }
    }
}