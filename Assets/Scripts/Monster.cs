using Scaling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterObject : MonoBehaviour
{
    public NavMeshAgent monster;

    public Transform target;

    public int initialHealth;
    public int lp;
    public float speed;

    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        initialHealth = lp;
        healthBar.value = healthBar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
       monster.SetDestination(target.position);

        if(lp <= 0)
        {
            Destroy(monster);
        }


    }

    void SetTarget(Transform target)
    {
        this.target = target;
    }

    void SetLp(int lp)
    {
        this.lp = lp;
    }

    void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
