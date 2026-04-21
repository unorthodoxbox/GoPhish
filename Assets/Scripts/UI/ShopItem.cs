using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopItem : MonoBehaviour
{
    private ShopPage shopPage;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnItemClicked);
    }

    public void Initialize(ShopPage page)
    {
        shopPage = page;
    }

    private void OnItemClicked()
    {
        if (shopPage != null)
            shopPage.ShowNotEnoughCoins();
        else
            Debug.LogError("shopPage is null on " + gameObject.name);
    }
}