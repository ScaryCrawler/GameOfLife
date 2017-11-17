using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace GameOfLife_Net
{
    public class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 1)
                throw new ArgumentException("Invalid argument usage");
            
            GameField gameField = new GameField();
            gameField.ReadFieldFromFile(args[0]);

            var field = gameField.InitializedField();

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    Console.Write(field[i,j]);
                }
                Console.WriteLine();
            }

            while (true)
            {
                Thread.Sleep(500);
                Console.Clear();
                field = gameField.PlayOneStep();
                for (int i = 0; i < field.GetLength(0); i++)
                {
                    for (int j = 0; j < field.GetLength(1); j++)
                    {
                        Console.Write(field[i, j]);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
