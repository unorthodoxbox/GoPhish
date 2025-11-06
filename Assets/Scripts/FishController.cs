using UnityEngine;

public class FishController : MonoBehaviour
{
    //used to change fish behavior based on world state
    public WorldStateData worldState;
    //public Transform spawnPoint;
    Renderer rend;


    Rigidbody rb;
    public float baseSpeed = -20.0f;
    public float speed = -20.0f;
    public float direction = 1.0f;
    private bool currentNight= false;
    private bool currentSafe = true;
    void Start()
    {
        //transform.position = spawnPoint.position;
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, baseSpeed,0);
        currentNight = worldState.isNightTime;
        currentSafe = worldState.isLocationSafe;
    }

     void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        //rend.material.color = worldState.isNightTime ? Color.black : Color.yellow;
        speed = worldState.isLocationSafe ? direction * baseSpeed:  direction * baseSpeed * 2;
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("ContainerWalls"))
        {
            direction = direction * -1;
            speed = speed * direction;
        }
    }
}
