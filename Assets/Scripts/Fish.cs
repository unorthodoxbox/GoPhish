using UnityEngine;

public class Fish : MonoBehaviour
{
    public int strength, chargeRate;
    public string fishType, pullState;
    public float exhaustedMult, tiredMult, desperateMult, stateChangeTime, stateChangeMin, stateChangeMax;
    public float curEnergy, maxEnergy, exhaustedEnergyLoss, tiredEnergyLoss, pullingEnergyLoss, desperateEnergyLoss;
    public float setStateChangeTime()
    {
        stateChangeTime = Random.Range(stateChangeMin, stateChangeMax);
        return stateChangeTime;
    }
    public bool runBrain(float stateChangeTimer)
    {
        if (stateChangeTimer >= stateChangeTime || pullState.Equals(""))
        {
            int intState = Random.Range(1 + Mathf.FloorToInt(curEnergy / 50), Mathf.RoundToInt(curEnergy / 10));
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
                curEnergy = Mathf.Clamp(curEnergy + (chargeRate - exhaustedEnergyLoss) * Time.deltaTime, 0f, maxEnergy);
                return strength * exhaustedMult;
            case "tired":
                curEnergy = Mathf.Clamp(curEnergy + (chargeRate - tiredEnergyLoss) * Time.deltaTime, 0f, maxEnergy);
                return strength * tiredMult;
            case "pulling":
                curEnergy = Mathf.Clamp(curEnergy + (chargeRate - pullingEnergyLoss) * Time.deltaTime, 0f, maxEnergy);
                return strength;
            case "desperate":
                curEnergy = Mathf.Clamp(curEnergy + (chargeRate - desperateEnergyLoss) * Time.deltaTime, 0f, maxEnergy);
                return strength * desperateMult;
        }
        return strength;
    }
}
