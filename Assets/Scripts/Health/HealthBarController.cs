using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Health
{
    public class HealthBarController : MonoBehaviour
    {

        private float _initialScale;
        private PlayerHealth _playerHealth;
    
        void Start()
        {
            _initialScale = transform.localScale.x;
            _playerHealth = FindFirstObjectByType<PlayerHealth>();
        
        
        
            _playerHealth.AddOnHealthUpdate(health =>
            {
                transform.localScale = new Vector3((_initialScale / _playerHealth.initialHealth) * health, transform.localScale.y,
                    transform.localScale.z);
            });
        }
    }
}
