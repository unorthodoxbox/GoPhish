using UnityEngine;

[CreateAssetMenu(fileName = "WorldStateData", menuName = "Settings/WorldStateData")]
public class WorldStateData : ScriptableObject
{
    public bool isNightTime = false;
    public bool isLocationSafe = true;
    public static float initialTimer = 10.0f;
    public static int initialSpawnCount = 2;


    public int totalSpawnCount = initialSpawnCount;
    public float timer = initialTimer;


    public float getInititalTimer() { 
        return initialTimer; 
    }
    public void changeTimeOfDay()
    {
        resetTotalSpawnCount();
        isNightTime = !isNightTime;
    }

    public void changeLocationSafety()
    {
        isLocationSafe = !isLocationSafe;
    }

    public void resetTimer()
    {
        timer = initialTimer;
    }

    public void changeTimer(float time)
    {
        timer += time;
    }

    public void changeTotalSpawnCount(int count)
    {
        totalSpawnCount += count;
    }
    
    public void resetTotalSpawnCount()
    {
        totalSpawnCount = initialSpawnCount;
    }
}
