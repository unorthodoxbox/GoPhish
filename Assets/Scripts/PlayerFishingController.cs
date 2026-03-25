using UnityEngine;

public class PlayerFishingController : MonoBehaviour
{
    public float maxFishFindTime, lineStrength, lineLength, pullStrength;
    public string curStance;

    public float getPullStrength(float fishPull)
    {
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
}
