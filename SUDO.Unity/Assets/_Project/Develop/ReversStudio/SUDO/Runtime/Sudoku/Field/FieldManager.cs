using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ReversStudio.SUDO.Runtime.Sudoku.Field
{
    public class FieldManager : MonoBehaviour
    {
        public int selectedNumber = -1; 

        [HideInInspector]
        public List<CellView> cellViews;

        private FieldGenerator _fieldGenerator;

        private int[,] _field;

        private List<CellView> _highlightedCells = new List<CellView>();


        [Button]
        private void Start()
        {
            Load();
        }

        private void Load()
        {
            _fieldGenerator = new FieldGenerator();
            LoadField();
        }

        private void LoadField()
        {
            _field = _fieldGenerator.GenerateField();
            FillCells(_field);
        }

        private void FillCells(int[,] field)
        {
            int cellCounter = 0;
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    cellViews[cellCounter].ChangeNumber(field[i-1,j-1]);
                    cellCounter++;
                }
            }
        }

        public void HightlightCells(int number)
        {
            if (_highlightedCells != null)
            {
                foreach (var highlightedCell in _highlightedCells)
                {
                    highlightedCell.ChangeCellState(false);
                }
                _highlightedCells.Clear();  
            }

            _highlightedCells = cellViews.FindAll(c => c.number == number);
            foreach (var highlightedCell in _highlightedCells)
            {
                highlightedCell.ChangeCellState(true);
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
