using UnityEngine;

public class TrialController : MonoBehaviour
{

    //used to change trial visibility based on world state
    public WorldStateData worldState;
    MeshRenderer meshRend;
    Collider col;

    private bool currentNight= false;
    private bool currentSafe = true;
    void Start()
    {
        meshRend = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();

        currentNight = worldState.isNightTime;
        currentSafe = worldState.isLocationSafe;
        meshRend.enabled = currentNight;
        col.enabled = currentNight;
    }

     void Update()
    {
        Debug.Log("Renderer enabled: " + meshRend.enabled + ", Night: " + worldState.isNightTime);

        if (currentNight != worldState.isNightTime){
            currentNight = worldState.isNightTime;
            meshRend.enabled = currentNight;
            col.enabled = currentNight;
        }
    }
}
