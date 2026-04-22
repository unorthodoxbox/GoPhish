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
        keyboard = Keyboard.current; 
        inventory.InitializeInventory(inventorySize);
    }

    private void Update()
    {
        if (keyboard == null) return; 

        if (keyboard.tabKey.wasPressedThisFrame)
        {
            if (!inventory.gameObject.activeSelf)
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
