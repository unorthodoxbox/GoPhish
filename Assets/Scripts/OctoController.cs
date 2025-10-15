using UnityEngine;

public class OctoController : MonoBehaviour
{

    //used to change octopus visibility based on world state
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
        meshRend.enabled = !currentNight;
        col.enabled = !currentNight;
    }

     void Update()
    {
        if (currentNight != worldState.isNightTime){
            currentNight = worldState.isNightTime;
            meshRend.enabled = !currentNight;
            col.enabled = !currentNight;
        }
    }
}
