using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class FieldSettings : MonoBehaviour
    {
        public void ChangeScale(float value)
        {
            Vector3 scale = transform.localScale;
            scale.x += value;
            scale.y += value;
            scale.z += value;
            scale.x += Math.Clamp(scale.x, 0.1f, float.MaxValue);
            scale.y += Math.Clamp(scale.y, 0.1f, float.MaxValue);
            scale.z += Math.Clamp(scale.z, 0.1f, float.MaxValue);
            transform.localScale = scale;
        }

        public void ChangeRotation(float value)
        {
            transform.Rotate(Vector3.up * value, Space.Self);
        }
    }
}