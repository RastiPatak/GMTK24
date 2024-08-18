using System;
using UnityEngine;

namespace Health
{
    public class Damager : MonoBehaviour
    {

        public int radius = 10;
        public int damagePerCycle = 2;
        public float cycleSeconds = 2;

        private float _secondsLeft;
        private GameObject _playerObject;
        private PlayerHealth _playerHealth;
        private MonsterObject _monsterObject;
        private Animator _animator;

        private void Start()
        {
            _secondsLeft = cycleSeconds;
            _playerObject = GameObject.FindGameObjectWithTag("Player");
            _playerHealth = _playerObject.GetComponent<PlayerHealth>();
            _monsterObject = GetComponent<MonsterObject>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_playerObject == null)
                return;

            if (_monsterObject.IsDowned)
                return;
            
            _secondsLeft -= Time.deltaTime;

            if (_secondsLeft <= 0.0f)
            {
                _secondsLeft = cycleSeconds;

                float distance = (transform.position - _playerObject.transform.position).sqrMagnitude;

                if (distance <= radius)
                {
                    _animator.Play("Attack");
                    _playerHealth.TakeDamage(damagePerCycle);
                    _animator.Play("Run");
                }

            }
        }
    }
}