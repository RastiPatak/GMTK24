using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WaveController : MonoBehaviour
{
    [SerializeField] GameObject Monster;
    [SerializeField] Transform Target;

    int WaveCounter;
    int StandardLp = 1; 

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
        //change lp

    }

    void StartWave()
    {
        int MonsterAmount =  WaveCounter + 1;

        for (int i = 0; i < MonsterAmount; i++)
        {
            SpawnMonster(StandardLp);
        }
        //spawn monster every 2 seconds
    }

    void SpawnMonster(int lp)
    {
        int SpawnPointY = 3;
        int SpawnPointX = Random.Range(-40, 40);
        int SpawnPointZ = Random.Range(-40, 40);

        Vector3 SpawnPosition = new Vector3(SpawnPointX, SpawnPointY, SpawnPointZ);

        GameObject go = Instantiate(Monster, SpawnPosition, Quaternion.identity);
        go.SendMessage("SetTarget", Target);
        go.SendMessage("SetLp", lp);

    }

}
