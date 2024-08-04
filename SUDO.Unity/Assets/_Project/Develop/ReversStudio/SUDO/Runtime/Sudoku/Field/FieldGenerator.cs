using System;
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

            FieldIsCorrect();
        
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

        private void DeleteNumbersFromField()
        {
            
        }

        private int SolveSudoku()
        {
            int steps = 0;

            
            
            return steps;
        }
        
        private bool FieldIsCorrect()
        {
            // rows check
            for (int k = 0; k < 9; k++)
            {
                for (int i = 0; i < 9; i++)
                {
                    int selectedNumber = _field[k, i];
                    for (int j = 0; j < 9; j++)
                    {
                        if (j!=i && selectedNumber == _field[k, j])
                        {
                            Debug.Log("поле неверно");
                            return false;
                        }
                    }
                }    
            }

            // column check
            for (int k = 0; k < 9; k++)
            {
                for (int i = 0; i < 9; i++)
                {
                    int selectedNumber = _field[i, k];
                    for (int j = 0; j < 9; j++)
                    {
                        if (j!=i && selectedNumber == _field[j, k])
                        {
                            Debug.Log("поле неверно");
                            return false;
                        }
                    }
                }    
            }
        
            // box check
            for (int a = 0; a < 7; a+=3)
            {
                for (int b = 0; b < 7; b+=3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            int selectedNumber = _field[i+a, j+b];
                            for (int l = 0; l < 3; l++)
                            {
                                for (int m = 0; m < 3; m++)
                                {
                                    if (l != i | m != j)
                                    {
                                        if (selectedNumber == _field[l+a, m+b])
                                        {
                                            Debug.Log("поле неверно");
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }                      
                }
            }
            return true;
        }
    }
}
