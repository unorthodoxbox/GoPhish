using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopPage : MonoBehaviour
{
    [SerializeField]
    private ShopItem itemPrefab;

    [SerializeField]
    private RectTransform Content;

    [SerializeField]
    private NotEnoughCoins notEnoughCoinsUI;
    
    List<ShopItem> listOfUIItems = new List<ShopItem>();

    public void InitializeShopUI(int shopsize)
    {
        for (int i = 0; i < shopsize; i++)
        {
            ShopItem uiItem = Instantiate(itemPrefab, Content, false);
            uiItem.Initialize(this);
            listOfUIItems.Add(uiItem);
        }
    }

    public void ShowNotEnoughCoins()
    {
        if (notEnoughCoinsUI != null)
        {
            notEnoughCoinsUI.ShowMessage();
        }
    }

    public void Show()
    {
        if (!gameObject.activeSelf)
        {
            bool opened = UICursorManager.TryOpenWindow(gameObject);
            if (opened)
            {
                gameObject.SetActive(true);
            }
        }
    }

    public void Hide()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            UICursorManager.CloseWindow(gameObject);
        }
    }
}
