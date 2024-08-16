using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    [SerializeField] GameObject Monster;
    [SerializeField] Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnMonster()
    {
        int SpawnPointY = 1;
        int SpawnPointX = Random.Range(-40, 40);
        int SpawnPointZ = Random.Range(-40, 40);

        Vector3 SpawnPosition = new Vector3(SpawnPointX, SpawnPointY, SpawnPointZ);

        GameObject go =  Instantiate(Monster, SpawnPosition, Quaternion.identity);
        go.SendMessage("SetTarget", Target);

    }
}
