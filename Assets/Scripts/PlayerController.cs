using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Transform model;
    public InputActions input;

    private Vector2 moveDirection = Vector2.zero;

    private CameraController camControl;

    private void Awake()
    {
        input = new InputActions();
        camControl = GetComponentInChildren<CameraController>();
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.Move.performed += OnMovementPerformed;
        input.Player.Move.canceled += OnMovementCanceled;
    }
    private void OnDisable()
    {
        input.Disable();
    }
    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>().normalized;
    }
    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        moveDirection = Vector2.zero;
    }
    private void FixedUpdate()
    {
        if (moveDirection != Vector2.zero)
        {
            Vector3 camForward = camControl.transform.forward;
            Vector3 camRight = camControl.transform.right;
            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 finalMoveDir = camForward * moveDirection.y + camRight * moveDirection.x;

            model.rotation = Quaternion.LookRotation(finalMoveDir);
            transform.position += finalMoveDir * moveSpeed * Time.fixedDeltaTime;
        }
    }
}