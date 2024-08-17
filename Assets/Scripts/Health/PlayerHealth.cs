using System;
using JetBrains.Annotations;
using UnityEngine;


namespace Health
{
    public class PlayerHealth : MonoBehaviour
    {
        private int _health = 50;

        public int Health
        {
            get => _health;
            private set
            {
                _health = value <= 0 ? 0 : value;
                _onUpdate?.Invoke(_health);
            }
        }

        public int initialHealth;
    
        [CanBeNull] private Action<int> _onUpdate;
        [CanBeNull] private Action _onDeath;
        
    
    
    
        private void Start()
        {
            initialHealth = Health;
        
            AddOnHealthUpdate(health =>
            {
                if (health <= 0)
                {
                    _onDeath?.Invoke();
                }
            });
        
            AddOnDeath(() =>
            {
                Debug.Log("Player died");
            });
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public void AddOnHealthUpdate(Action<int> onUpdate)
        {
            _onUpdate += onUpdate;
        }

        public void AddOnDeath(Action onDeath)
        {
            _onDeath += onDeath;
        }

        public void UnsubscribeAllOnHealthUpdates()
        {
            _onUpdate = null;
        }

        public void UnsubscribeOnDeath()
        {
            _onDeath = null;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                TakeDamage(1);
            }
        }
    }
}