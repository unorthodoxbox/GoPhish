using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;

public class HUDScript : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private ShopPage shopPage;
    private Keyboard keyboard;

    private void Start()
    {
        keyboard = Keyboard.current;
        StartCoroutine(UpdateTime());
    }

    private IEnumerator UpdateTime()
    {
        while (true)
        {
            timeText.text = DateTime.Now.ToString("h:mm tt");
            yield return new WaitForSeconds(1f);
        }
    }

    private void Update()
    {
        if (keyboard == null) return;

        if (keyboard.gKey.wasPressedThisFrame)
        {
            ToggleShop();
        }
    }

    private void ToggleShop()
    {
        if (shopPage == null)
        {
            Debug.LogError("shopPage is null!");
            return;
        }

        if (!shopPage.gameObject.activeSelf)
        {
            shopPage.Show();
        }
        else
        {
            shopPage.Hide();
        }
    }

    public void CloseShop()
    {
        shopPage.Hide();
    }
}