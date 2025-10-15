using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] fishPrefabs;
    //public GameObject currentPrefabs;
    public GameObject currentFish;
    public WorldStateData worldState;
    public Transform spawnPoint;
    public float spawnDelay = 4.0f;

    private bool currentNight= false;
    private bool currentSafe = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentNight = worldState.isNightTime;
        currentSafe = worldState.isLocationSafe;
        StartCoroutine(WaitToSpawn());
        //SpawnFish();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentNight != worldState.isNightTime){
            currentNight = worldState.isNightTime;
            StartCoroutine(WaitToSpawn());
            //SpawnFish();
        }
    }

    IEnumerator WaitToSpawn(){        
        // pauses for 4 seconds before spawning fish 
        DespawnFish();
        yield return new WaitForSeconds(spawnDelay);

        SpawnFish();
    }

    void DespawnFish(){
        if (currentFish != null){
            Destroy(currentFish);
        }
        worldState.changeTimer(4.0f);
    }  

    void SpawnFish(){
        //StartCoroutine(waitToSpawn());
        //worldState.timer += 4.0f;

        /*if (currentFish != null){
            Destroy(currentFish);
        }*/

        //checks WorldState to determine which fish to spawn
        GameObject newFish = null;
        if (worldState.isNightTime){
            newFish  = fishPrefabs[1];
        } else {
            newFish  = fishPrefabs[0];
        }
        currentFish = Instantiate(newFish, spawnPoint.position, Quaternion.identity);
        currentFish.transform.position = spawnPoint.position;

    }

}


//currentFish.SetActive(false);

 /* //checks if fish already exists, if not spawns a new one
        if (currentFish == null){
            currentFish = Instantiate(newFish, spawnPoint.position, Quaternion.identity);
        } else {
            //currentFish.SetActive(true);
            currentFish.transform.position = spawnPoint.position;
        }*/
