using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace ReversStudio.SUDO.Runtime.Sudoku.Field
{
    public class FieldGenerator
    {
        private readonly int[,] CorrectField = new int[9, 9]
        {
            {1,2,3, 4,5,6,  7,8,9},
            {4,5,6, 7,8,9,  1,2,3},
            {7,8,9, 1,2,3,  4,5,6},
        
            {2,3,4, 5,6,7,  8,9,1},
            {5,6,7, 8,9,1,  2,3,4},
            {8,9,1, 2,3,4,  5,6,7},
        
            {3,4,5, 6,7,8,  9,1,2},
            {6,7,8, 9,1,2,  3,4,5},
            {9,1,2, 3,4,5,  6,7,8}
        };

        private static readonly Random random = new Random();

        private int[,] _field;

        private FieldVerifier _fieldVerifier;

        public void Init(FieldVerifier fieldVerifier)
        {
            _fieldVerifier = fieldVerifier;
        }
    
        public int[,] GenerateField()
        {
            _field = CorrectField;

        
            for (int i = 0; i < 1000; i++)
            {
                switch(random.Next(0,5))
                {
                    case 0:
                        SwapColumnArea();
                        break;
                    case 1:
                        SwapRowsSmall();
                        break;
                    case 2:
                        SwapRowsArea();
                        break;
                    case 3:
                        SwapColumnSmall();
                        break;
                    case 4:
                        TransposeMatrix();
                        break;
                }
            }
            
            FieldVerifier fieldVerifier = new FieldVerifier();
            Debug.Log(fieldVerifier.FieldIsCorrect(_field));

            RemoveCellsAndCheck(_field,40);
            
            return _field;
        }

        private void SwapRowsSmall()
        {
            int randomArea = random.Next(0, 3) * 3;
            int randomRow = random.Next(1,3);

            int rowFirst = randomArea;
            int rowSecond = randomArea+randomRow;
            for (int i = 0; i < 9; i++)
            {
                (_field[rowFirst, i], _field[rowSecond, i]) = (_field[rowSecond, i], _field[rowFirst, i]);
            }

        }
    
        private void SwapRowsSmall(int rowFirst, int rowSecond)
        {
            for (int i = 0; i < 9; i++)
            {
                (_field[rowFirst, i], _field[rowSecond, i]) = (_field[rowSecond, i], _field[rowFirst, i]);
            }
        }
    
        private void SwapColumnSmall(int columnFist, int columnSecond)
        {
            for (int i = 0; i < 9; i++)
            {
                (_field[i, columnFist], _field[i, columnSecond]) = (_field[i, columnSecond], _field[i, columnFist]);
            }
        }
    
        private void SwapColumnSmall()
        {
            int randomArea = random.Next(0, 3) * 3;

            int columnFirst = randomArea;
            int columnSecond = randomArea+random.Next(1,3);
            for (int i = 0; i < 9; i++)
            {
                (_field[i, columnFirst], _field[i, columnSecond]) = (_field[i, columnSecond], _field[i, columnFirst]);
            }
        }
    
        private void SwapRowsArea()
        {
            int randomAreaRowOne = random.Next(0, 3) * 3;

            int offsetSecondRow = 0;
        
            switch (randomAreaRowOne)
            {
                case 0:
                    offsetSecondRow = random.Next(1, 3) * 3;
                    break;
                case 3:
                    offsetSecondRow = Convert.ToBoolean(random.Next(-1, 1)) ? -3 : +3;
                    break;
                case 6:
                    offsetSecondRow = -random.Next(1, 3) * 3;
                    break;
            }

            for (int i = 0; i < 3; i++)
            {
                SwapRowsSmall(randomAreaRowOne+i, randomAreaRowOne + offsetSecondRow+i);
            }
        }
    
        private void SwapColumnArea()
        {
            int randomAreaColumnOne = random.Next(0, 3) * 3;

            int offsetSecondColumn = 0;
        
            switch (randomAreaColumnOne)
            {
                case 0:
                    offsetSecondColumn = random.Next(1, 3) * 3;
                    break;
                case 3:
                    offsetSecondColumn = Convert.ToBoolean(random.Next(-1, 1)) ? -3 : +3;
                    break;
                case 6:
                    offsetSecondColumn = -random.Next(1, 3) * 3;
                    break;
            }

            for (int i = 0; i < 3; i++)
            {
                SwapColumnSmall(randomAreaColumnOne+i, randomAreaColumnOne + offsetSecondColumn+i);
            }
        }
    
        private void TransposeMatrix()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = i; j < 9; j++)
                {
                    (_field[i, j],_field[j, i]) = (_field[j, i],_field[i, j]);
                }
            }
        }
        
        private int[,] CellsRemover(int removeQuantity)
        {
            int[,] tempField = _field;
            int x = random.Next(0, 9);
            int y = random.Next(0, 9);

            tempField[y, x] = -1;

            for (int i = 0; i < removeQuantity; i++)
            {
                if (GetMissedCellsInBox(x, y, tempField) == 0)
                {
                    
                }
            }






            return tempField;
        }

        private int GetMissedCellsInBox(int x, int y, int[,] tempField)
        {
            int count = 0;

            if (x >= 6)
            {
                x = 6;
            }
            else
            {
                if (x >= 3)
                {
                    x = 3;
                }
                else
                {
                    if (x >= 0) x = 0;
                }
            }
            
            
            if (y >= 6)
            {
                y = 6;
            }
            else
            {
                if (y >= 3)
                {
                    y = 3;
                }
                else
                {
                    if (y >= 0) y = 0;
                }
            }
            
            for (int i = y; i < y+3; i++)
            {
                for (int j = x; j < x+3; j++)
                {
                    if (tempField[i, j] == -1)
                    {
                        count++;
                    }
                }
            }
            Debug.Log(count);
            return count;
        }
        
        
        // Метод для удаления клеток и проверки решаемости
        public void RemoveCellsAndCheck(int[,] board, int cellsToRemove)
        {
            int size = board.GetLength(0);
            List<(int, int, int)> removedCells = new List<(int, int, int)>();

            for (int i = 0; i < cellsToRemove; i++)
            {
                int row, col;
                do
                {
                    row = random.Next(size);
                    col = random.Next(size);
                }
                while (board[row, col] == 0);

                int temp = board[row, col];
                board[row, col] = 0;
                removedCells.Add((row, col, temp));

                // Если после удаления клеток Судоку не решаемо, возвращаем клетку назад
                if (!IsSolvable(board))
                {
                    board[row, col] = temp;
                    removedCells.RemoveAt(removedCells.Count - 1);
                }
            }
        }

        // Метод для проверки решаемости доски Судоку
        private static bool IsSolvable(int[,] board)
        {
            int size = board.GetLength(0);
            int[,] boardCopy = (int[,])board.Clone();
            return Solve(boardCopy) && !HasMultipleSolutions(boardCopy);
        }

        // Решение Судоку методом Backtracking
        private static bool Solve(int[,] board)
        {
            int row = -1;
            int col = -1;
            bool isEmpty = true;
            int size = board.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j] == 0)
                    {
                        row = i;
                        col = j;
                        isEmpty = false;
                        break;
                    }
                }
                if (!isEmpty) break;
            }

            if (isEmpty) return true;

            for (int num = 1; num <= size; num++)
            {
                if (IsValid(board, row, col, num))
                {
                    board[row, col] = num;
                    if (Solve(board)) return true;
                    board[row, col] = 0;
                }
            }
            return false;
        }

        // Проверка корректности вставки числа
        private static bool IsValid(int[,] board, int row, int col, int num)
        {
            for (int i = 0; i < 9; i++)
            {
                if (board[row, i] == num || board[i, col] == num ||
                    board[row - row % 3 + i / 3, col - col % 3 + i % 3] == num)
                {
                    return false;
                }
            }
            return true;
        }

        // Проверка на наличие нескольких решений
        private static bool HasMultipleSolutions(int[,] board)
        {
            int row = -1;
            int col = -1;
            bool isEmpty = true;
            int size = board.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j] == 0)
                    {
                        row = i;
                        col = j;
                        isEmpty = false;
                        break;
                    }
                }
                if (!isEmpty) break;
            }

            if (isEmpty) return false;

            int solutionsFound = 0;
            for (int num = 1; num <= size; num++)
            {
                if (IsValid(board, row, col, num))
                {
                    board[row, col] = num;
                    if (Solve(board))
                    {
                        solutionsFound++;
                        if (solutionsFound > 2) return true;
                    }
                    board[row, col] = 0;
                }
            }
            return false;
        }
    }
}
