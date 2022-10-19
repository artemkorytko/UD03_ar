using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Bot : Entity
    {
        private const float DELAY = 1.5f;
        
        public override void GetStep(params CellType[] field)
        {
            StartCoroutine(ChooseStep(field));
        }

        private IEnumerator ChooseStep(CellType[] field)
        {
            yield return new WaitForSeconds(DELAY);

            List<int> freeCells = new List<int>();

            for (int i = 0; i < field.Length; i++)
            {
                if (field[i]==CellType.None)
                {
                    freeCells.Add(i);
                }
            }
            
            OnStep?.Invoke(freeCells[Random.Range(0, freeCells.Count)], playWith);
        }
    }
}