using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] pickupRef;
    [SerializeField] private Transform[] spawnLoc;

    private int randomIndex;
    private int randomSpawnIndex;
    private GameObject spawnedPickup;
    void Start()
    {
        StartCoroutine(SpawnPickups());
    }

    IEnumerator SpawnPickups()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));

            randomIndex = Random.Range(0,pickupRef.Length);
            randomSpawnIndex = Random.Range(0,spawnLoc.Length);

            //spawnedPickup = Instantiate(
            //    pickupRef[randomIndex],
            //    spawnLoc[randomSpawnIndex].position,
            //    Quaternion.identity
            //);
            if(spawnLoc[randomSpawnIndex].childCount ==0)
            {
                spawnedPickup = Instantiate(pickupRef[randomIndex], spawnLoc[randomSpawnIndex]);
                spawnedPickup.transform.SetPositionAndRotation(spawnLoc[randomSpawnIndex].position, Quaternion.identity);
            }
            


        }
    }
}
