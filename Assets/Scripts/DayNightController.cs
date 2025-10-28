using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public WorldStateData worldState;
    public GameObject directionalLight;
    private Light lightComponent;
    public Color dayColor;
    //public float timer = 10.0f;


    //when testing restart:
    void Awake() {
        // worldState.timer = worldState.initialTimer;
        worldState.resetTimer();
        worldState.resetTotalSpawnCount();
        worldState.isNightTime = false;
        worldState.isLocationSafe = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lightComponent = directionalLight.GetComponent<Light>();
        dayColor = lightComponent.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        worldState.changeTimer(-Time.deltaTime);
        //worldState.timer -= Time.deltaTime;
        lightComponent.color = worldState.isNightTime ? Color.red : dayColor;
        if (worldState.timer <= 0)
        {
            worldState.changeTimeOfDay();
            worldState.resetTimer();
        }
        //worldState.timer = worldState.timer <= 0 ? 10.0f : worldState.timer;
    }
}
