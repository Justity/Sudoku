using ReversStudio.SUDO.Runtime.Sudoku.Field;
using UnityEngine;

namespace ReversStudio.SUDO.Runtime.Sudoku
{
    public class NumberSelector : MonoBehaviour
    {
        [SerializeField]
        private FieldManager fieldManager;

        private int _lastNumber;
    
        public void OnNumberChanged(int number)
        {
            if(_lastNumber==number)
                return;

            _lastNumber = number;
            
            fieldManager.HighlightCells(number);
        }
    }
}
