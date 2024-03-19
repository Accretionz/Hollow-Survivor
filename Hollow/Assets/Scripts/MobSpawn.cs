using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject meleeMob;
    [SerializeField]
    private GameObject rangedMob;
    [SerializeField]
    private float minSpawnTime;
    [SerializeField]
    private float maxSpawnTime;
    [SerializeField]
    private float numMobs;
    [SerializeField]
    private Camera managedCamera;

    private float timeUntilSpawn;
    private float Timer;

    void Start()
    {
        setRandomSpawnTime();
    }
    
    void Update()
    {
        Timer += Time.deltaTime;
        timeUntilSpawn -= Time.deltaTime;
        if (timeUntilSpawn <= 0)
        {
            for (int i=0; i< numMobs; i++)
            {
                int randSide = Random.Range(0, 4);
                Vector3 spawnLocation;
                if(randSide == 0)
                {
                    // Spawn from left side.
                    spawnLocation = managedCamera.ViewportToWorldPoint(new Vector3(0, Random.Range(0.0f, 1.0f), managedCamera.nearClipPlane));
                }
                else if(randSide == 1)
                {
                    // Spawn from right side.
                    spawnLocation = managedCamera.ViewportToWorldPoint(new Vector3(1, Random.Range(0.0f, 1.0f), managedCamera.nearClipPlane));
                }else if (randSide == 2)
                {
                    // Spawn from bottom side.
                    spawnLocation = managedCamera.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 0, managedCamera.nearClipPlane));
                }
                else 
                {
                    // Spawn from top side.
                    spawnLocation = managedCamera.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 1, managedCamera.nearClipPlane));
                }
                NavMeshHit hit;
                UnityEngine.AI.NavMesh.SamplePosition(spawnLocation, out hit, Mathf.Infinity, UnityEngine.AI.NavMesh.AllAreas);
                spawnLocation = hit.position;
                spawnLocation.z = 0;
                Instantiate(this.rangedMob, spawnLocation, Quaternion.identity);
            }
            setRandomSpawnTime();
        }
    }

    void setRandomSpawnTime()
    {
        timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }
}