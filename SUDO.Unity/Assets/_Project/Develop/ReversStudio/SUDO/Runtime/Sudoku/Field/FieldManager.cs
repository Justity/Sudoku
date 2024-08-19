using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ReversStudio.SUDO.Runtime.Sudoku.Field
{
    public class FieldManager : MonoBehaviour
    {
        public int[] selectedIndex;
        private int _selectedNumber = -1;

        [HideInInspector]
        public List<CellView> cellViews;

        private int[] _quantityOfNumber = new int[9];
        
        private FieldGenerator _fieldGenerator;

        private int[,] _field;
        private int[,] _userField;

        private List<CellView> _highlightedCells = new List<CellView>();

        private FieldVerifier _fieldVerifier;
        
        private void Start()
        {
            Init();
            GenerateField();
        }


        public void Init()
        {
            _fieldVerifier = new FieldVerifier();
            _fieldGenerator = new FieldGenerator();
            _fieldGenerator.Init(_fieldVerifier);
            foreach (var cellView in cellViews)
            {
                cellView.Init(this);
            }
        }

        private void GenerateField()
        {
            for (int i = 0; i < _quantityOfNumber.Length; i++)
            {
                _quantityOfNumber[i] = 0;
            }
            _field = _fieldGenerator.GenerateField();
            _userField = (int[,])_field.Clone();
            FillCells(_field);
        }

        private void FillCells(int[,] field)
        {
            int cellCounter = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (_field[i, j] != 0)
                    {
                        _quantityOfNumber[field[i, j]-1]++;
                    }
                    cellViews[cellCounter].LoadNumber(field[i,j],new[]{i,j});
                    cellCounter++;
                }
            }
        }

        public void HighlightCells(int number)
        {
            _selectedNumber = number;
            
            foreach (var highlightedCell in _highlightedCells)
            {
                highlightedCell.ChangeCellSelectedState(false);
            }
            _highlightedCells.Clear();  

            _highlightedCells = cellViews.FindAll(c => c.number == _selectedNumber);
            foreach (var highlightedCell in _highlightedCells)
            {
                highlightedCell.ChangeCellSelectedState(true);
            }
        }

        public void TrySetNumber(CellView cell, int[] index)
        {
            if (_selectedNumber != -1)
            {
                cell.ChangeNumber(_selectedNumber);
                _userField[index[0], index[1]] = _selectedNumber;
                
                _highlightedCells.Add(cell);
                cell.ChangeCellSelectedState(true);
                
                _quantityOfNumber[_selectedNumber - 1]++;

                CheckCompleted();
            }
        }
        
        public void RemoveNumber(CellView cell, int[] index)
        {
            if (_selectedNumber == cell.number)
            {
                cell.ResetState();

                cell.ChangeNumber(0);
                _userField[index[0], index[1]] = 0;
                cell.ChangeCellSelectedState(false);
                _quantityOfNumber[_selectedNumber - 1]--;
                UnCheckCompleted();
            }
        }

        private void CheckCompleted()
        {
            if (_quantityOfNumber[_selectedNumber-1] >= 9)
            {
                foreach (var cell in cellViews.FindAll(c=>c.number==_selectedNumber))
                {
                    cell.CompleteState(true);
                }
            }
        }

        private void UnCheckCompleted()
        {
            for (int i = 0; i < _quantityOfNumber.Length; i++)
            {
                if (_quantityOfNumber[i] < 9)
                {
                    foreach (var cell in cellViews.FindAll(c => c.number == i + 1))
                    {
                        cell.CompleteState(false);
                    }
                }
            }
        }

#if UNITY_EDITOR
        [SerializeField]
        private GameObject[] area;

        [Button]
        private void FillCellViews()
        {
            cellViews.Clear();
            for (int r = 0; r < 7; r += 3)
            {
                for (int k = 0; k < 7; k += 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            cellViews.Add(area[i+r].transform.GetChild(j + k).GetComponent<CellView>());
                        }
                    }
                }
            }
        }
#endif
    }
}
