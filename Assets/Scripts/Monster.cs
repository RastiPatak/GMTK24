using Scaling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.UI;

public class MonsterObject : MonoBehaviour
{
    public NavMeshAgent monster;

    public Transform target;

    public int lp;
    public float speed;

    private Resizable _resizable;
    public int initialHealth;

    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        
        _resizable = GetComponent<Resizable>();
        
        //both based on the round and scaling
        //set hp
        //set speed
        //var a = GetComponent<Resizable>();
        //a.Resize(Lp);
        initialHealth = lp;
        healthBar.value = healthBar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
       monster.SetDestination(target.position);

        if(lp <= 0)
        {
            Destroy(gameObject);
            Destroy(monster);
        }


    }

    private void UpdateHealth()
    {
        lp = (int) Math.Round(lp * _resizable.HealthMultiplier);
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
