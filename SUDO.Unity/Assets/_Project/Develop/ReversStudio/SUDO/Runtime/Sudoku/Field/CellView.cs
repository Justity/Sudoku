using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ReversStudio.SUDO.Runtime.Sudoku.Field
{
    public class CellView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textMeshPro;

        [SerializeField]
        private Image image;

        [SerializeField]
        private Color selectedColor;
        
        [SerializeField]
        private Color defaultColor;

        public int number = -1;

        public void ChangeNumber(int numberToSet)
        {
            number = numberToSet;
            textMeshPro.text = number.ToString();
        }


        public void ChangeCellState(bool isSelected)
        {
            image.color = isSelected ? selectedColor : defaultColor;
        }
    }
}
