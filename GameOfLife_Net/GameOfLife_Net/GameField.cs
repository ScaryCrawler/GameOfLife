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
            for (int i = 1; i < rowCount - 1; i++)
            {
                for (int j = 1; j < colCount - 1; j++)
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

                    _currentStepArray[i, j] = _nextStepArray[i, j];
                }
            }

        }

        // TODO: need to implement game field like a tours
        private int[] GetNeighbors(int i, int j) =>
            new[]
            {
                _currentStepArray[i - 1, j - 1],
                _currentStepArray[i - 1, j],
                _currentStepArray[i - 1, j + 1],
                _currentStepArray[i, j - 1],
                _currentStepArray[i, j + 1],
                _currentStepArray[i + 1, j - 1],
                _currentStepArray[i + 1, j],
                _currentStepArray[i + 1, j + 1]
            };

        private int CountOfNeighbor(int[] neighbors) => neighbors.Count(x => x == 1);
    }
}
