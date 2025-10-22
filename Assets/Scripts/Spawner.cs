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

    private bool currentNight = false;
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
        if (currentNight != worldState.isNightTime)
        {
            currentNight = worldState.isNightTime;
            StartCoroutine(WaitToSpawn());
        } else if (SpawnCycle())
        {
            StartCoroutine(WaitToSpawn());
        }
    }

    IEnumerator WaitToSpawn()
    {
        worldState.changeTotalSpawnCount(-1);
        // pauses for 4 seconds before spawning fish 
        DespawnFish();
        yield return new WaitForSeconds(spawnDelay);
        SpawnFish();
    }

    void DespawnFish()
    {
        if (currentFish != null)
        {
            Destroy(currentFish);
        }
        worldState.changeTimer(4.0f);
    }

    void SpawnFish()
    {
        //checks WorldState to determine which fish to spawn
        GameObject newFish = null;
        if (worldState.isNightTime)
        {
            newFish = fishPrefabs[1];
            switch (GetRandomCase())
            {
                case 1:
                    newFish = fishPrefabs[3]; //ultra fish
                    break;
                case 2:
                    newFish = fishPrefabs[2]; //trash
                    break;
                case 3:
                    newFish = fishPrefabs[1]; //normal fish
                    break;
            }
        }
        else
        {
            switch (GetRandomCase())
            {
                case 1:
                    newFish = fishPrefabs[3]; //ultra fish
                    break;
                case 2:
                    newFish = fishPrefabs[2]; //trash
                    break;
                case 3:
                    newFish = fishPrefabs[0]; //normal fish
                    break;
            }
            //newFish = fishPrefabs[0];
        }
        currentFish = Instantiate(newFish, spawnPoint.position, Quaternion.identity);
        currentFish.transform.position = spawnPoint.position;
    }

    bool SpawnCycle()
    {
        bool spawn = false;
        if (currentNight == worldState.isNightTime) {
            if (worldState.timer <=  (worldState.getInititalTimer() / 2.0) && worldState.totalSpawnCount > 0)
                spawn = true;
        }
        return spawn;
    }

    int GetRandomCase()
    {
        int outcome = 0;
        // random probabilities: .10 for trash; .05 for gold/ultra fish; rest for normal fish 
        int ran = Random.Range(0, 100);
        if (ran < 33)
        {
            outcome = 1; //ultra
        }
        else if (ran < 66)
        {
            outcome = 2; //trash 
        }
        else
        {
            outcome = 3; //normal fish
        }
        return outcome;
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
