using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopPage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private ShopItem itemPrefab;

    [SerializeField]
    private RectTransform Content;
    
    List<ShopItem> listOfUIItems = new List<ShopItem>();

    public void InitializeShopUI(int shopsize)
    {
	    // Instantiate multiple item slots inside the scrollable content panel
        for (int i = 0; i < shopsize; i++)
        {
            ShopItem uiItem = Instantiate(itemPrefab, Content, false);
            listOfUIItems.Add(uiItem);
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
