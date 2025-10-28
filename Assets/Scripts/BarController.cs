using UnityEngine;
using UnityEngine.InputSystem;

public class BarController : MonoBehaviour
{

    Rigidbody rb;
    private Inputs inputs;

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

}
