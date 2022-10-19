using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class Player : Entity
    {
        private Camera _arCamera;

        private void Awake()
        {
            _arCamera = FindObjectOfType<Camera>();
        }

        public override void GetStep(params CellType[] field)
        {
            StartCoroutine(ChooseStep(field));
        }

        private IEnumerator ChooseStep(CellType[] field)
        {
            while (true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 touchPosition = Input.mousePosition;
                    Ray ray = _arCamera.ScreenPointToRay(touchPosition);

                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        Cell cell = hit.collider.gameObject.GetComponent<Cell>();

                        if (cell != null && cell.IsActive)
                        {
                            OnStep?.Invoke(cell.Index, playWith);
                            break;
                        }
                    }
                }

                yield return null;
            }
        }
    }
}