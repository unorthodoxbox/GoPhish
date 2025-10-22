using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private InventoryItem itemPrefab;

    [SerializeField]
    private RectTransform inventoryDisplay;

    // List for storing all UI items
    private List<InventoryItem> itemList = new List<InventoryItem>();

    public void InitializeInventory(int inventorySize)
    {
        // Clear existing children if reinitializing
        foreach (Transform child in inventoryDisplay)
        {
            Destroy(child.gameObject);
        }

        itemList.Clear();

        // Instantiate all item slots correctly under the UI hierarchy
        for (int i = 0; i < inventorySize; i++)
        {
            InventoryItem item = Instantiate(itemPrefab, inventoryDisplay, false);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;
            itemList.Add(item);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
