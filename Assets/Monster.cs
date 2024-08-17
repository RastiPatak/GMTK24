using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    public NavMeshAgent Monster;

    public Transform Target;

    int Lp;

    void SetTarget(Transform target)
    {
        Target = target;
    }

    void SetLp(int lp)
    {
        Lp = lp;
    }

    // Start is called before the first frame update
    void Start()
    {
        //both based on the round and scaling
        //set hp
        //set speed
    }

    // Update is called once per frame
    void Update()
    {
        Monster.SetDestination(Target.position);
    }
}
