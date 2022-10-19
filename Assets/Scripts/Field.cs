using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Field : MonoBehaviour
    {
        private Cell[] _cells;
        public event Action<CellType> OnCompleted;

        private void Awake()
        {
            _cells = GetComponentsInChildren<Cell>();
        }

        public void Initialize()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i].Initialize(i);
            }
        }

        public void SetState(bool state)
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i].IsActive = state;
            }
        }

        public void Refresh()
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i].Refresh();
            }
        }

        public void SetCell(int index, CellType type)
        {
            if (index < 0 || index >= _cells.Length) return;

            switch (type)
            {
                case CellType.Cross:
                    _cells[index].SelectCross();
                    break;
                case CellType.Zero:
                    _cells[index].SelectZero();
                    break;
            }

            Check();
        }

        private void Check()
        {
            if (!CheckLines(out CellType type))
            {
                OnCompleted?.Invoke(type);
            }

            if (!CheckCount())
            {
                OnCompleted?.Invoke(CellType.None);
            }
        }

        private bool CheckCount()
        {
            int unusedCount = 0;

            for (int i = 0; i < _cells.Length; i++)
            {
                if (_cells[i].Type == CellType.None)
                {
                    unusedCount++;
                }
            }

            if (unusedCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckLines(out CellType winType)
        {
            winType = CellType.None;

            CellType cellType = CellType.None;

            //check rows
            for (int i = 0; i < _cells.Length; i += 3)
            {
                cellType = _cells[i].Type;
                bool result = false;

                for (int j = i + 1; j < i + 3; j++)
                {
                    if (cellType != _cells[j].Type)
                    {
                        result = true;
                        break;
                    }
                }

                if (!result && cellType != CellType.None)
                {
                    winType = cellType;
                    return result;
                }
            }

            //check column
            for (int i = 0; i < 3; i++)
            {
                cellType = _cells[i].Type;
                bool result = false;

                for (int j = i + 3; j < _cells.Length; j += 3)
                {
                    if (cellType != _cells[j].Type)
                    {
                        result = true;
                        break;
                    }
                }

                if (!result && cellType != CellType.None)
                {
                    winType = cellType;
                    return result;
                }
            }

            //check diagonal 1
            {
                cellType = _cells[0].Type;
                bool result = false;

                for (int j = 4; j < _cells.Length; j += 4)
                {
                    if (cellType != _cells[j].Type)
                    {
                        result = true;
                        break;
                    }
                }

                if (!result && cellType != CellType.None)
                {
                    winType = cellType;
                    return result;
                }
            }

            //check diagonal 2
            {
                cellType = _cells[2].Type;
                bool result = false;

                for (int j = 4; j < _cells.Length - 1; j += 2)
                {
                    if (cellType != _cells[j].Type)
                    {
                        result = true;
                        break;
                    }
                }

                if (!result && cellType != CellType.None)
                {
                    winType = cellType;
                    return result;
                }
            }
            return true;
        }

        public CellType[] FieldToArray()
        {
            CellType[] intField = new CellType[_cells.Length];

            for (int i = 0; i < _cells.Length; i++)
            {
                intField[i] = _cells[i].Type;
            }

            return intField;
        }
    }
}