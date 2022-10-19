using System;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected CellType playWith = CellType.None;

        public Action<int, CellType> OnStep;

        public abstract void GetStep(params CellType[] field);
    }
}