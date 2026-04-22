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

    private bool hasWarnedMissingFishingBar;
    private bool hasWarnedMissingStrengthFill;

    private void Awake()
    {
        CacheFishingUIRefs();
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
        CacheFishingUIRefs();

        if (fish == null || player == null)
        {
            return;
        }

        fishStateUI.text = "Fish State: " + fish.GetComponent<Fish>().pullState;
        playerStanceUI.text = "Player Strength: " + Mathf.RoundToInt(100 * player.curPullPercent) + "%";
        fishDistanceUI.text = "Fish Distance: " + Mathf.Ceil(fishDistance);
        curIntegrityPercentUI.text = "Line Integrity: " + Mathf.RoundToInt(100 * (player.curIntegrity / player.maxIntegrity)) + "%";
        fishEnergyUI.text = "Fish Energy: " + Mathf.RoundToInt(100 * (fish.GetComponent<Fish>().curEnergy / fish.GetComponent<Fish>().maxEnergy)) + "%";

        //adjusts fishing bar according to how close the fish is
        if (fishDistanceBarUI != null)
        {
            float distance = (20 - fishDistance) / 20;
            fishDistanceBarUI.offsetMax = new Vector2((distance * 385) - 235, fishDistanceBarUI.offsetMax.y);
        }
        else if (!hasWarnedMissingFishingBar)
        {
            Debug.LogWarning("FishingManager could not find a fishing distance bar RectTransform on fishingUI. Text will still update.");
            hasWarnedMissingFishingBar = true;
        }

        //adjusts strengthBar according to player strength
        if (strengthFillUI != null)
        {
            strengthFillUI.offsetMax = new Vector2(strengthFillUI.offsetMax.x, (player.curPullPercent * 255) - 155);
        }
        else if (!hasWarnedMissingStrengthFill)
        {
            Debug.LogWarning("FishingManager could not find a strength fill RectTransform on fishingUI. Text will still update.");
            hasWarnedMissingStrengthFill = true;
        }
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

    private void CacheFishingUIRefs()
    {
        if (fishingUI == null)
        {
            return;
        }

        if (fishDistanceBarUI == null)
        {
            fishDistanceBarUI = FindChildRectTransform(fishingUI.transform, "fishDistanceBar")
                ?? FindChildRectTransform(fishingUI.transform, "fishingBar");
        }

        if (strengthFillUI == null)
        {
            strengthFillUI = FindChildRectTransform(fishingUI.transform, "strengthFill");
        }
    }

    private RectTransform FindChildRectTransform(Transform root, string childName)
    {
        Transform child = root.Find(childName);
        if (child != null)
        {
            return child as RectTransform;
        }

        for (int i = 0; i < root.childCount; i++)
        {
            RectTransform match = FindChildRectTransform(root.GetChild(i), childName);
            if (match != null)
            {
                return match;
            }
        }

        return null;
    }
}
