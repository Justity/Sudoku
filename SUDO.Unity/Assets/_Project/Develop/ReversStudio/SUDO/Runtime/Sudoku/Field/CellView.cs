using DG.Tweening;
using ReversStudio.SUDO.Runtime.Helpers;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ReversStudio.SUDO.Runtime.Sudoku.Field
{
    public class CellView : MonoBehaviour
    {
        public int number = -1;

        public bool isUserCell = false;

        [SerializeField]
        private NumbersViewConfig numbersViewConfig;

        [SerializeField]
        private Material[] materials;

        [SerializeField]
        private Material[] materialsUser;

        [SerializeField]
        private TextMeshProUGUI textMeshPro;

        [SerializeField]
        private Image image;

        [SerializeField]
        private Color selectedColor;

        [SerializeField]
        private Color defaultColor;

        [SerializeField]
        private Color wrongColor;

        private FieldManager _fieldManager;

        private int[] _indexInArray;

        private bool _isSelected = false;
        private bool _isCompleted = false;
        private bool _isWrong = false;

        public void Init(FieldManager fieldManager)
        {
            _fieldManager = fieldManager;
        }

        public void LoadNumber(int numberToSet, int[] indexInArray)
        {
            _indexInArray = indexInArray;
            number = numberToSet;
            
            if (numberToSet == 0)
            {
                textMeshPro.text = "";
                isUserCell = true;
                return;
            }

            isUserCell = false;
            textMeshPro.fontMaterial = materials[0];
            textMeshPro.text = number.ToString();
        }

        public void ChangeNumber(int numberToSet)
        {
            if (isUserCell)
            {
                number = numberToSet;

                if (numberToSet == 0)
                {
                    textMeshPro.text = "";
                }
                else
                {
                    textMeshPro.fontMaterial = materialsUser[numberToSet];
                    textMeshPro.text = number.ToString();
                }
            }
        }

        public void CellOnClick()
        {
            if (isUserCell)
            {
                if (textMeshPro.text == "")
                {
                    _fieldManager.TrySetNumber(this, _indexInArray);
                }
                else
                {
                    _fieldManager.RemoveNumber(this, _indexInArray);
                }
            }
        }

        public void ResetState()
        {
            _isWrong = false;
            _isCompleted = false;
            _isSelected = false;
            DefaultView();
        }
        public void WrongState(bool state)
        {
            _isWrong = state;
            if (_isWrong)
            {
                WrongView();
            }
        }

        public void CompleteState(bool state)
        {
            _isCompleted = state;
            if (_isCompleted)
            {
                CompletedView();
            }
            else
            {
                if (_isSelected)
                {
                    SelectedView();
                }
                else
                {
                    DefaultView();
                }
            }
        }
        
        public void ChangeCellSelectedState(bool isSelected)
        {
            _isSelected = isSelected;
            if (_isWrong)
                return;
            
            if (_isSelected)
            {
                if (_isCompleted)
                {
                    CompletedView();
                    return;
                }
                SelectedView();
            }
            else
            {
                DefaultView();
            }
        }


        private void DefaultView()
        {
            image.DOColor(defaultColor, .5f);
            textMeshPro.fontMaterial = isUserCell ? materialsUser[0] : materials[0];
        }

        private void SelectedView()
        {
            image.DOColor(selectedColor, .5f);
            textMeshPro.fontMaterial = isUserCell ? materialsUser[number] : materials[number];
        }
        
        private void CompletedView()
        {
            image.DOColor(numbersViewConfig.numberConfigs[number-1].completedColor, .5f);
        }

        private void WrongView()
        {
            _isWrong = true;
            image.DOColor(wrongColor, .5f);
        }

    }
}
