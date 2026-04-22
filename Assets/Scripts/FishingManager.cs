using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using TMPro;

public class FishingManager : MonoBehaviour
{
    public float curStress, fishDistance;

    public bool isFishing;
    public List<GameObject> fishTypes = new List<GameObject>();
    public float fishFindTimer, deltaFishDistance;
    public float fishStateChangeTimer, fishStateChangeTime;

    public TMP_Text fishStateUI, playerStanceUI, fishDistanceUI, curIntegrityPercentUI, fishEnergyUI;
    public RectTransform fishDistanceBarUI, strengthFillUI;
    public GameObject fish, fishingUI;
    public PlayerFishingController player;

    private void Awake()
    {
        if (fishDistanceBarUI == null && fishingUI != null)
        {
            Transform barTransform = fishingUI.transform.Find("fishDistanceBar");
            Transform barTransform2 = fishingUI.transform.Find("strengthFill");
            if (barTransform != null && barTransform2 != null)
            {
                fishDistanceBarUI = barTransform as RectTransform;
                strengthFillUI = barTransform2 as RectTransform;
            }
        }
    }

    void Update()
    {
        if (isFishing)
        {
            doFishing();
        }
    }
    public void startCast()
    {
        //this will change to actually take player input later
        startFishing(10);
    }
    public void startFishing(float castDistance)
    {
        fishFindTimer = 0f;
        fishStateChangeTimer = 0f;
        fishStateChangeTime = 0f;
        isFishing = true;
        fishDistance = castDistance;
        fishingUI.SetActive(true);
        player.curIntegrity = player.maxIntegrity;
        player.curPullPercent = 0.5f;
    }

    private void doFishing()
    {
        if (fish == null)
        {
            fishFindTimer += Time.deltaTime;
            if (Random.Range(0f, player.maxFishFindTime) <= fishFindTimer)
            {
                int newFish = Random.Range(0, fishTypes.Count);
                fish = GameObject.Instantiate(fishTypes[newFish], new Vector3(12.22f, 4.59f, -18.93f), Quaternion.identity);
                fish.transform.LookAt(player.transform);
                fishFindTimer = 0f;
            }
        }
        else
        {
            fishStateChangeTimer += Time.deltaTime;
            if (fish.GetComponent<Fish>().runBrain(fishStateChangeTimer))
            {
                fishStateChangeTimer = 0;
                fishStateChangeTime = fish.GetComponent<Fish>().setStateChangeTime();
            }

            calcLineStress();
            if (curStress >= player.lineStrength)
            {
                player.curIntegrity -= curStress * Time.deltaTime;
            }
            else
            {
                player.curIntegrity -= 0.5f * Time.deltaTime;
            }
            updateUI();

            checkCatch();
            checkBreak();
        }
    }
    public void calcLineStress()
    {
        float fishPull = fish.GetComponent<Fish>().getPullStrength();
        float playerPull = player.getPullStrength(fishPull, fishDistance);
        curStress = playerPull + fishPull;
        deltaFishDistance = (fishPull - playerPull) * Time.deltaTime;
        fishDistance = Mathf.Clamp(fishDistance + deltaFishDistance, 0f, player.lineLength);
    }
    public void updateUI()
    {
        if (fishDistanceBarUI == null)
        {
            return;
        }

        fishStateUI.text = "Fish State: " + fish.GetComponent<Fish>().pullState;
        playerStanceUI.text = "Player Strength: " + Mathf.RoundToInt(100 * player.curPullPercent) + "%";
        fishDistanceUI.text = "Fish Distance: " + Mathf.Ceil(fishDistance);
        curIntegrityPercentUI.text = "Line Integrity: " + Mathf.RoundToInt(100 * (player.curIntegrity / player.maxIntegrity)) + "%";
        fishEnergyUI.text = "Fish Energy: " + Mathf.RoundToInt(100 * (fish.GetComponent<Fish>().curEnergy / fish.GetComponent<Fish>().maxEnergy)) + "%";

        //adjusts fishing bar according to how close the fish is
        float distance = (20 - fishDistance)/20;
        fishDistanceBarUI.offsetMax = new Vector2((distance * 385) - 235, fishDistanceBarUI.offsetMax.y);

        //adjusts strengthBar according to player strength
        strengthFillUI.offsetMax = new Vector2(strengthFillUI.offsetMax.x, (player.curPullPercent * 255) - 155);
    }
    public void checkCatch()
    {
        if (fishDistance <= 0f)
        {
            endFishing();
            Debug.Log("Congration, you got a fish :)");
        }
    }
    public void checkBreak()
    {
        if (player.curIntegrity <= 0)
        {
            breakLine();
        }
    }
    public void breakLine()
    {
        endFishing();
        Debug.Log("Your line broke and the fish escaped :(");
    }
    public void endFishing()
    {
        Destroy(fish);
        isFishing = false;
        fishingUI.SetActive(false);
    }
}
