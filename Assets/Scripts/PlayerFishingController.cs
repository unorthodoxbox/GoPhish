using UnityEngine;
using UnityEngine.AdaptivePerformance;
using UnityEngine.InputSystem;

public class PlayerFishingController : MonoBehaviour
{
    public float maxFishFindTime, lineStrength, lineLength, pullStrength, maxIntegrity, curIntegrity;
    public string curStance = "hold";
    public bool canFish = false;
    public GameObject fishPrompt;
    public FishingManager FM;

    private void Update()
    {
        stanceCheck();
        if (canFish && Keyboard.current.eKey.wasPressedThisFrame)
        {
            fishPrompt.SetActive(false);
            FM.startCast();
        }
    }
    public float getPullStrength(float fishPull, float fishDistance)
    {
        if (fishDistance == 0)
        {
            curStance = "hold";
            return fishPull;
        }
        switch (curStance)
        {
            case "freespool":
                return 0f;
            case "hold":
                if (pullStrength >= fishPull)
                {
                    return fishPull;
                }
                else
                {
                    return pullStrength;
                }
            case "reel":
                return pullStrength;
        }
        return 0f;
    }
    private void stanceCheck()
    {
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            switch (curStance)
            {
                case "hold":
                    curStance = "freespool";
                    break;
                case "reel":
                    curStance = "hold";
                    break;
            }
        }
        else if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            switch (curStance)
            {
                case "freespool":
                    curStance = "hold";
                    break;
                case "hold":
                    curStance = "reel";
                    break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        canFish = true;
        fishPrompt.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        canFish = false;
        fishPrompt.SetActive(false);
    }
}
