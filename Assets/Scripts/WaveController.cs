using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.AI.Navigation.Editor;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class WaveController : MonoBehaviour
{
    [SerializeField] GameObject Monster;
    [SerializeField] Transform Target;

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
        int DefaultWaveLp = 5 + WaveCounter;
        float DefaultWaveSpeed = 1;
        int MonsterAmount =  WaveCounter;

        for (int i = 0; i < MonsterAmount; i++)
        {
            SpawnMonster(DefaultWaveLp, DefaultWaveSpeed);
        }
        //spawn monster every 2 seconds
    }

    void SpawnMonster(int lp, float speed)
    {
        int SpawnPointY = 3;
        int SpawnPointX = Random.Range(-40, 40);
        int SpawnPointZ = Random.Range(-40, 40);

        Vector3 SpawnPosition = new Vector3(SpawnPointX, SpawnPointY, SpawnPointZ);

        GameObject monster = Instantiate(Monster, SpawnPosition, Quaternion.identity);

        monster.SendMessage("SetTarget", Target);
        monster.SendMessage("SetLp", lp);
        monster.SendMessage("SetSpeed", speed);
    }

}
