using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
//custom class
public class Wave
{
    public string waveName;
    public int noOfEnemy;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}


public class wavespwanner : MonoBehaviour
{
   public  Wave[] waves;
    public Transform[] spawnpoint;

    private Wave currentwave;
    private int currentWaveNumber;

    private float nextspawnTime;

    private bool canspawn = true;

    private void Update()
    {
        currentwave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalenemies = GameObject.FindGameObjectsWithTag("enemy");
        if(totalenemies.Length ==0 && !canspawn&& currentWaveNumber+1 !=waves.Length)
        {
            currentWaveNumber++;
            canspawn = true;
        }
    
    
    }

    void SpawnWave()
    {
        if(canspawn && nextspawnTime<Time.time) {
            GameObject randomEnemy = currentwave.typeOfEnemies[Random.Range(0, currentwave.typeOfEnemies.Length)];
            Transform randomPoint = spawnpoint[Random.Range(0, spawnpoint.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentwave.noOfEnemy--;
            nextspawnTime = Time.time + currentwave.spawnInterval;
            if(currentwave.noOfEnemy == 0)
            {
                canspawn = false;
            }
        
        
        }
     
    }
}
