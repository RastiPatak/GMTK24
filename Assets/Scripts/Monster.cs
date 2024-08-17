using Scaling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterObject : MonoBehaviour
{
    public NavMeshAgent Monster;

    public Transform Target;

    public int Lp;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        //both based on the round and scaling
        //set hp
        //set speed
        //var a = GetComponent<Resizable>();
        //a.Resize(Lp);
    }

    // Update is called once per frame
    void Update()
    {
       Monster.SetDestination(Target.position);

        if(Lp <= 0)
        {
            Destroy(Monster);
        }
    }

    void SetTarget(Transform target)
    {
        Target = target;
    }

    void SetLp(int lp)
    {
        Lp = lp;
    }

    void SetSpeed(float speed)
    {
        Speed = speed;
    }
}
