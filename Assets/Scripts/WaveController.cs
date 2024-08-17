using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.AI.Navigation.Editor;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using static UnityEngine.GraphicsBuffer;

public class WaveController : MonoBehaviour
{
    public GameObject monster;
    public Transform target;

    public int WaveCounter { get; set; } = 2;

    // Start is called before the first frame update
    void Start()
    {
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        //check if the wave ended
        //change lp

    }

    void StartWave()
    { 
        WaveCounter++;
        int defaultWaveLp = 5 + WaveCounter;
        float defaultWaveSpeed = 1;
        int monsterAmount =  WaveCounter;

        for (int i = 0; i < monsterAmount; i++)
        {
            SpawnMonster(defaultWaveLp, defaultWaveSpeed);
        }
        //spawn monster every 2 seconds
    }

    void SpawnMonster(int lp, float speed)
    {
        int spawnPointY = 3;
        int spawnPointX = Random.Range(-40, 40);
        int spawnPointZ = Random.Range(-40, 40);

        Vector3 spawnPosition = new Vector3(spawnPointX, spawnPointY, spawnPointZ);

        GameObject monster = Instantiate(this.monster, spawnPosition, Quaternion.identity);
        
        MonsterObject monsterObject = monster.GetComponent<MonsterObject>();

        monsterObject.target = target;
        monsterObject.lp = lp;
        monsterObject.speed = speed;

    }

}
