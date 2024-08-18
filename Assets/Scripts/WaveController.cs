using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Health;
using Unity.AI.Navigation.Editor;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using static UnityEngine.GraphicsBuffer;
using TMPro;

public class WaveController : MonoBehaviour
{
    public GameObject monster;
    public Transform target;

    public TextMeshProUGUI waveCounterText;

    public int waveCounter { get; set; } = 0;

    private bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartWave());
        waveCounterText.text = "Wave Nr: " + waveCounter;
    }

    // Update is called once per frame
    void Update()
    {
        
        int monsterCount = GameObject.FindGameObjectsWithTag("Monster").Length;
        
        if(monsterCount == 0 && start)
        {
            StartCoroutine(StartWave());
            start = false;
        }



    }

    IEnumerator StartWave()
    {
        yield return new WaitForSeconds(5);

        waveCounter++;
        waveCounterText.text = "Wave Nr: " + waveCounter;
        start = true;
        Debug.Log(waveCounter);
        int defaultWaveLp = 5 + (waveCounter * 2);
        float defaultWaveSpeed = 1;
       
        for (int i = 0; i < (waveCounter + 1) * 2; i++)
        {
            SpawnMonster(defaultWaveLp, defaultWaveSpeed); 
        }
    }

    void SpawnMonster(int lp, float speed)
    {
        int spawnPointY = 3;
        int spawnPointX = Random.Range(-38, 38);
        int spawnPointZ = Random.Range(-38, 38);

        Vector3 spawnPosition = new Vector3(spawnPointX, spawnPointY, spawnPointZ);

        GameObject monster = Instantiate(this.monster, spawnPosition, Quaternion.identity);
        
        MonsterObject monsterObject = monster.GetComponent<MonsterObject>();

        monsterObject.target = target;
        monsterObject.lp = lp;
        monsterObject.speed = speed;


    }

}
