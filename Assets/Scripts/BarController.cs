using UnityEngine;
using UnityEngine.InputSystem;

public class BarController : MonoBehaviour
{

    Rigidbody rb;
    private Inputs inputs;
    private bool fishContact = false;
    public ProgressBar progressBar;

    //public float baseSpeed = -10.0f;
    public float upSpeed = 10.0f;
    //public float direction = 1.0f;
    //private bool spacePressed= false;
    //private bool currentSafe = true;


    private void Awake()
    {
        inputs = new Inputs();
    }


    private void OnEnable()
    {
        inputs.Player.Enable();
        inputs.Player.Continue.performed += MoveBar;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    void FixedUpdate()
    {
        if (fishContact)
        {
            progressBar.IncreaseProgress(0.2f);
        }
     //
    }

    private void OnDisable()
    {
        inputs.Player.Continue.performed -= MoveBar;
        inputs.Player.Disable();
    }


    void MoveBar(InputAction.CallbackContext context)
    {
        rb.velocity = new Vector3(0, 0f, 0);
        rb.AddForce(Vector3.up * upSpeed, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BouncerObject"))
        {
            fishContact = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BouncerObject"))
        {
            fishContact = false;
        }
    }

}
