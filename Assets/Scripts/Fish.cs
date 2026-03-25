using UnityEngine;

public class Fish : MonoBehaviour
{
    public int energy, strength, chargeRate;
    public string type, fishType, pullState;
    public float exhaustedMult, tiredMult, desperateMult, stateChangeTime, stateChangeMin, stateChangeMax;
    public void setStateChangeTime()
    {
        stateChangeTime = Random.Range(stateChangeMin, stateChangeMax);
    }
    public bool runBrain(float stateChangeTimer)
    {
        if (stateChangeTimer >= stateChangeTime)
        {
            int intState = Random.Range(1, 5);
            switch (intState)
            {
                case 1:
                    pullState = "exhausted";
                    break;
                case 2:
                    pullState = "tired";
                    break;
                case 3:
                    pullState = "pulling";
                    break;
                case 4:
                    pullState = "desperate";
                    break;
            }
            return true;
        }
        return false;
    }
    public float getPullStrength()
    {
         switch (pullState)
        {
            case "exhausted":
                return strength * exhaustedMult;
            case "tired":
                return strength * tiredMult;
            case "pulling":
                return strength;
            case "desperate":
                return strength * desperateMult;
        }
        return strength;
    }
}
