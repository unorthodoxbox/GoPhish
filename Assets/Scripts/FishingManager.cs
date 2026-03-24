using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class FishingManager : MonoBehaviour
{
    public float curStress;

    public bool isFishing;
    public List<GameObject> fishTypes = new List<GameObject>();
    private float fishTimer = 0f;

    public GameObject fish;
    public PlayerFishingController player;

    // Update is called once per frame
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
            fishTimer += Time.deltaTime;
            if (Random.Range(0f, player.guaranteedFishTime) <= fishTimer)
            {
                int newFish = Random.Range(0, fishTypes.Count);
                fish = GameObject.Instantiate(fishTypes[newFish]);
            }
        }
        else
        {
            
        }
    }
}
