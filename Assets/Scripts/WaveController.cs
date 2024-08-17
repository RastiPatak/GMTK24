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

    public int waveCounter { get; set; } = 0;
    public int monsterAmount { get; set; } = -1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartWave());
    }

    // Update is called once per frame
    void Update()
    {
        if(monsterAmount == 0)
        {
            StartCoroutine(StartWave());
        }

    }

    IEnumerator StartWave()
    {
        yield return new WaitForSeconds(5);
        waveCounter++;
        monsterAmount = waveCounter + 1;
        int defaultWaveLp = 5 + waveCounter;
        float defaultWaveSpeed = 1;
       
        for (int i = 0; i <= monsterAmount; i++)
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
