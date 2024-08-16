using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] GameObject MonsterSpawner;
    int WaveCounter;

    // Start is called before the first frame update
    void Start()
    {
        WaveCounter = 3;
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        //check if the wave ended

    }

    void StartWave()
    {
        int MonsterAmount =  WaveCounter + 1;

        for (int i = 0; i < MonsterAmount; i++)
        {
            MonsterSpawner.SendMessage("SpawnMonster");
        }
        //spawn monster every 2 seconds
    }
}
