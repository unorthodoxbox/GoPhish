using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;

public class FishingManager : MonoBehaviour
{
    public float curStress, fishDistance;

    public bool isFishing;
    public List<GameObject> fishTypes = new List<GameObject>();
    public float fishFindTimer;
    public float fishStateChangeTimer, fishStateChangeTime;

    public GameObject fish;
    public PlayerFishingController player;

    void Update()
    {
        if (isFishing)
        {
            doFishing();
        }
    }

    private void doFishing()
    {
        if (fish == null)
        {
            fishFindTimer += Time.deltaTime;
            if (Random.Range(0f, player.maxFishFindTime) <= fishFindTimer)
            {
                int newFish = Random.Range(0, fishTypes.Count);
                fish = GameObject.Instantiate(fishTypes[newFish]);
                fishFindTimer = 0f;
            }
        }
        else
        {
            if (fishStateChangeTime == 0)
            {
                fish.GetComponent<Fish>().setStateChangeTime();
            }
            fishStateChangeTimer += Time.deltaTime;
            if (fish.GetComponent<Fish>().runBrain(fishStateChangeTimer))
            {
                fishStateChangeTimer = 0;
            }

            calcLineStress();

            checkBreak();
        }
    }
    public void calcLineStress()
    {
        float fishPull = fish.GetComponent<Fish>().getPullStrength();
        float playerPull = player.getPullStrength(fishPull);
        curStress = playerPull + fishPull;
        float deltaFishDistance = fishPull - playerPull;
        fishDistance += deltaFishDistance;
    }
    public void checkBreak()
    {
        if (curStress > player.lineStrength)
        {
            breakLine();
        }
    }
    public void breakLine()
    {
        //Might move this to PlayerFishingController
        Debug.Log("Not yet implemented");
    }
}
