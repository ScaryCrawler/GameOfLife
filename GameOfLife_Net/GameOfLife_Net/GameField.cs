using System;
using System.IO;
using System.Linq;

namespace GameOfLife_Net
{
    public class GameField
    {
        private int[,] _currentStepArray;
        private int[,] _nextStepArray;

        private int rowCount;
        private int colCount;

        public void ReadFieldFromFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                var buffer = reader.ReadToEnd();
                var rows = buffer.Split('\n').Select(x => x.Split(' ')).ToList();

                rowCount = rows.Count;
                colCount = rows[0].Length;

                _currentStepArray = new int[rowCount, colCount];
                _nextStepArray = new int[rowCount, colCount];

                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        _currentStepArray[i, j] = Int32.Parse(rows[i][j]);
                        _nextStepArray[i, j] = _currentStepArray[i, j];
                    }
                }
            }
        }

        public string[,] InitializedField()
        {
            return GetFieldState();
        }


        public string[,] PlayOneStep()
        {
            NextState();
            return GetFieldState();
        }

        private string[,] GetFieldState()
        {
            string[,] gameField = new string[rowCount, colCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    gameField[i, j] = _currentStepArray[i, j] == 1 ? " # " : " - ";
                }
            }

            return gameField;
        }

        private void NextState()
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    var countOfNeighbors = CountOfNeighbor(GetNeighbors(i, j));

                    if (_currentStepArray[i, j] == 1)
                    {
                        if (countOfNeighbors == 2 || countOfNeighbors == 3)
                        {
                            _nextStepArray[i, j] = 1;
                        }
                        else _nextStepArray[i, j] = 0;
                    }
                    else
                    {
                        if (countOfNeighbors == 3)
                            _nextStepArray[i, j] = 1;
                    }
                }
            }

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    _currentStepArray[i, j] = _nextStepArray[i, j];
                }
            }
        }

        private int[] GetNeighbors(int i, int j)
        {
            int top = (i == 0) ? rowCount - 1 : i - 1;
            int bottom = (i == rowCount - 1) ? 0 : i + 1;
            int left = (j == 0) ? colCount - 1 : j - 1;
            int rigth = (j == colCount - 1) ? 0 : j + 1;

            return new[]
            {
                _currentStepArray[top, left],
                _currentStepArray[top, j],
                _currentStepArray[top, rigth],
                _currentStepArray[i, left],
                _currentStepArray[i, rigth],
                _currentStepArray[bottom, left],
                _currentStepArray[bottom, j],
                _currentStepArray[bottom, rigth]
            };
        }
        
        private int CountOfNeighbor(int[] neighbors) => neighbors.Count(x => x == 1);
    }
}
