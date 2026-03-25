using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public bool inventory;

    public float lookSpeed, minAngle, maxAngle;
    public InputActions input;
    public FishingManager FM;

    private float xRot = 0f;
    private float yRot = 0f;

    private void Awake()
    {
        input = new InputActions();
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.Look.performed += OnLook;
    }
    private void OnDisable()
    {
        input.Disable();
    }
    private void OnLook(InputAction.CallbackContext context)
    {
        //Grab the mouseDelta
        Vector2 lookInput = context.ReadValue<Vector2>();

        //calculate the new x and y rotations
        xRot -= lookInput.y;
        xRot = Mathf.Clamp(xRot, minAngle, maxAngle);
        yRot += lookInput.x;

        //apply the rotations
        transform.rotation = Quaternion.Euler(xRot, yRot, 0f);
    }
    private void Update()
    {
        if (inventory || Time.timeScale == 0f)
        {
            if (input.Player.enabled)
            {
                input.Disable();
            }
            if (Cursor.visible == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        else if (FM.isFishing && input.Player.enabled)
        {
            if (input.Player.enabled)
            {
                input.Disable();
            }
        }
        else
        {
            if (!input.Player.enabled)
            {
                input.Enable();
            }
            if (Cursor.visible == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
