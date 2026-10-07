using System;

namespace Lib_5
{
    public static class TaskSolver
    {
        public static double[] CalculateOddRowAverages(int[,] matrix)
        {
            int rowCount = matrix.GetLength(0);
            int columnCount = matrix.GetLength(1);
            int oddRowsCount = (rowCount + 1) / 2;
            double[] averageResults = new double[oddRowsCount];
            int resultIndex = 0;
            for (int i = 0; i < rowCount; i += 2)
            {
                int currentRowSum = 0;
                for (int j = 0; j < columnCount; j++)
                {
                    currentRowSum += matrix[i, j];
                } 
                averageResults[resultIndex] = (double)currentRowSum / columnCount;
                resultIndex++;
            }
            return averageResults;
        }
    }
}