using UnityEngine;
using UnityEngine.AdaptivePerformance;
using UnityEngine.InputSystem;

public class PlayerFishingController : MonoBehaviour
{
    public float maxFishFindTime, lineStrength, lineLength, maxPullStrength, maxIntegrity, curIntegrity, curPullPercent, sensitivity;
    public bool canFish = false;
    public GameObject fishPrompt;
    public FishingManager FM;

    private void Update()
    {
        pullCheck();
        if (canFish && Keyboard.current.eKey.wasPressedThisFrame)
        {
            fishPrompt.SetActive(false);
            FM.startCast();
        }
    }
    public float getPullStrength(float fishPull, float fishDistance)
    {
        float curPullStrength = curPullPercent * maxPullStrength;
        if (fishDistance == lineLength && curPullStrength < fishPull)
        {
            curPullStrength = fishPull;
        }
        return curPullStrength;
    }
    private void pullCheck()
    {
        if (Keyboard.current.downArrowKey.isPressed)
        {
            curPullPercent -= sensitivity * Time.deltaTime;
        }
        else if (Keyboard.current.upArrowKey.isPressed)
        {
            curPullPercent += sensitivity * Time.deltaTime;
        }
        curPullPercent = Mathf.Clamp(curPullPercent, 0f, 1f);
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
