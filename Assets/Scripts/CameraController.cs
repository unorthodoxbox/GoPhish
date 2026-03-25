using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float lookSpeed, minAngle, maxAngle;
    public InputActions input;

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
}
