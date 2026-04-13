using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField]
    private ShopPage shopUI;

    public int shopsize = 10;

    private void Start()
    {
        shopUI.InitializeShopUI(shopsize);
    }

}
