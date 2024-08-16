using System;
using UnityEngine;

namespace Scaling
{
    public class Resizable : MonoBehaviour
    {

        private float _currentScale = 1;

        public double HealthMultiplier => 2 * Math.Log(_currentScale) + 1f;
        
        public void Resize(float scale)
        {
            this._currentScale = scale;
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
        }

        public void Smaller()
        {
            this._currentScale -= .1f;
            this.Resize(this._currentScale);
        }

        public void Bigger()
        {
            this._currentScale += .1f;
            this.Resize(this._currentScale);
        }
    }
    
    
}
