using UnityEngine;

[CreateAssetMenu(fileName = "WorldStateData", menuName = "Settings/WorldStateData")]
public class WorldStateData : ScriptableObject
{
    public bool isNightTime = false;
    public bool isLocationSafe = true;
    public float timer = 10.0f;

    public void changeTimeOfDay()
    {
        isNightTime = !isNightTime;
    }

    public void changeLocationSafety()
    {
        isLocationSafe = !isLocationSafe;
    }

    public void resetTimer()
    {
        timer = 10.0f;
    }
    
    public void changeTimer(float time)
    {
        timer += time;
    }
}
