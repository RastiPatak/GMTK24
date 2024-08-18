using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimationHandler : MonoBehaviour
{

    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayHit()
    {
        _animator.Play("HitReceive", 1);
    }

    public void PlayDeath()
    {
        _animator.Play("Death");
    }
}
