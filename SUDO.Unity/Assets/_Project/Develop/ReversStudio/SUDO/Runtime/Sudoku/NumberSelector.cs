using ReversStudio.SUDO.Runtime.Sudoku.Field;
using UnityEngine;

namespace ReversStudio.SUDO.Runtime.Sudoku
{
    public class NumberSelector : MonoBehaviour
    {
        [SerializeField]
        private FieldManager fieldManager;
    
        public void OnNumberChanged(int number)
        {
            fieldManager.HightlightCells(number);
        }
    }
}
