using System;
using Scaling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class MonsterObject : MonoBehaviour
{
    public NavMeshAgent monster;

    public Transform target;

    public int initialHealth;
    public int maxHealth;
    public int lp;
    public float speed;

    public Slider healthBar;

    private Resizable _resizable;

    // Start is called before the first frame update
    void Start()
    {
        initialHealth = lp;
        maxHealth = lp;
        healthBar.maxValue = initialHealth;
        healthBar.value = healthBar.maxValue;
        
        _resizable = GetComponent<Resizable>();
        
        //both based on the round and scaling
        //set hp
        //set speed
        //var a = GetComponent<Resizable>();
        //a.Resize(Lp);
    }

    // Update is called once per frame
    void Update()
    {
       monster.SetDestination(target.position);

        if(lp <= 0)
        {
            WaveController wc =FindFirstObjectByType<WaveController>();
            wc.monsterAmount -= 1;

            Destroy(gameObject);
        }
    }
      
    private void UpdateHealth()
    {
        int maxBefore = maxHealth;
        maxHealth = (int) Math.Round(initialHealth * _resizable.HealthMultiplier);
        lp += (maxHealth - maxBefore);
        healthBar.maxValue = maxHealth;
        healthBar.value = lp;
    }

    private void UpdateSpeed()
    {
        speed *= (float)_resizable.HealthMultiplier;
        monster.speed = speed;
    }
    
    public void Resize(float scale)
    {
        _resizable.Resize(scale);
        UpdateHealth();
        UpdateSpeed();
    }

    public void Bigger()
    {
        _resizable.Bigger();
        UpdateHealth();
        UpdateSpeed();
    }

    public void Smaller()
    {
        _resizable.Smaller();
        UpdateHealth();
        UpdateSpeed();

    }

}
