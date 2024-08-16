using System;
using UnityEngine;

namespace Scaling
{
    public class ResizableTest : MonoBehaviour
    {
        private Resizable _resizable;

        private void Start()
        {
            this._resizable = GetComponent<Resizable>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _resizable.Bigger();
                Debug.Log($"Current health of {gameObject} is x{_resizable.HealthMultiplier}");
            }
        }
    }
}