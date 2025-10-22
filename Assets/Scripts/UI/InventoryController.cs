using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private InventoryUI inventory;

    public int inventorySize = 30;

    private Keyboard keyboard;

    private void Start()
    {
        keyboard = Keyboard.current; // Get reference to the keyboard
        inventory.InitializeInventory(inventorySize);
    }

    private void Update()
    {
        if (keyboard == null) return; // safety

        // Check for tab key press
        if (keyboard.tabKey.wasPressedThisFrame)
        {
            if (!inventory.isActiveAndEnabled)
            {
                inventory.Show();
            }
            else
            {
                inventory.Hide();
            }
        }
    }
}
