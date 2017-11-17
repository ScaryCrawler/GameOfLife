using System;
using System.IO;
using System.Linq;

namespace GameOfLife_Net
{
    public class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 1)
                throw new ArgumentException("Invalid argument usage");

            int[,] currentStepArray;
            int[,] nextStepArray;

            int rowCount;
            int colCount;

            using (StreamReader reader = new StreamReader(args[0]))
            {
                var buffer = reader.ReadToEnd();
                var rows = buffer.Split('\n').Select(x => x.Split(' ')).ToList();

                rowCount = rows.Count;
                colCount = rows[0].Length;

                currentStepArray = new int[rowCount, colCount];
                nextStepArray = new int[rowCount, colCount];

                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        currentStepArray[i, j] = Int32.Parse(rows[i][j]);
                        nextStepArray[i, j] = currentStepArray[i, j];
                    }
                }
            }
            

        }
    }
}
