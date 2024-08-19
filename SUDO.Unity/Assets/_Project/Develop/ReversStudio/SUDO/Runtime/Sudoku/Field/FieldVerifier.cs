using UnityEngine;

namespace ReversStudio.SUDO.Runtime.Sudoku.Field
{
    public class FieldVerifier
    {
        public bool FieldIsCorrect(int[,] field)
        {
            var isCorrect = CheckAllBoxes(field);
            if (isCorrect == false) return false;

            isCorrect = CheckAllRow(field);
            if (isCorrect == false) return false;
        
            isCorrect = CheckAllColumn(field);
            if (isCorrect == false) return false;
            
            return true;
        }

        private bool CheckAllRow(int[,] field)
        {
            // rows check
            for (int i = 0; i < 9; i++)
            {
                if (RowsIsCorrect(GetRow(field, i)) == false)
                {
                    return false;
                }
            }
            return true;
        }
        
        private bool CheckAllColumn(int[,] field)
        {
            // column check
            for (int i = 0; i < 9; i++)
            {
                if (ColumnIsCorrect(GetColumn(field, i)) == false)
                {
                    return false;
                }
            }
            return true;
        }
        
        
        private bool CheckAllBoxes(int[,] field)
        {
            // box check
            for (int a = 0; a < 7; a+=3)
            {
                for (int b = 0; b < 7; b+=3)
                {
                    if(BoxIsCorrect(GetBox(field, b, a)) == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        
        private bool RowsIsCorrect(int[] row)
        {
            for (int i = 0; i < 9; i++)
            {
                int selectedNumber = row[i];
                for (int j = 0; j < 9; j++)
                {
                    if (j == i || selectedNumber != row[j]) continue;
                    Debug.Log("поле неверно");
                    return false;
                }
            }

            return true;
        }

        private bool ColumnIsCorrect(int[] column)
        {
            for (int i = 0; i < 9; i++)
            {
                int selectedNumber = column[i];
                for (int j = 0; j < 9; j++)
                {
                    if (j==i || selectedNumber != column[j]) continue;
                    Debug.Log("поле неверно");
                    return false;
                }
            }

            return true;
        }

        private bool BoxIsCorrect(int[,] box)
        {
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    var selectedNumber = box[i, j];
                    for (var l = 0; l < 3; l++)
                    {
                        for (var m = 0; m < 3; m++)
                        {
                            if (l == i | m == j) continue;
                            if (selectedNumber != box[l, m]) continue;
                            Debug.Log("поле неверно");
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private int[] GetRow(int[,] field, int index)
        {
            var length = field.GetLength(0);
            int[] row = new int[length];

            for (var x = 0; x < length; x++)
            {
                row[x] = field[index, x];
            }
            
            return row;
        }
        
        private int[] GetColumn(int[,] field, int index)
        {
            var length = field.GetLength(1);
            int[] column = new int[length];
            
            for (var y = 0; y < length; y++)
            {
                column[y] = field[y,index];
            }
            
            return column;
        }
        
        private int[,] GetBox(int[,] field, int indexX, int indexY)
        {
            int[,] box = new int[3,3];
            
            for (var y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    box[x, y] = field[indexX+y, indexY+x];
                }
            }
            
            return box;
        }
    }
}
