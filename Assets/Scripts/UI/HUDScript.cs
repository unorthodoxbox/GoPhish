using TMPro;
using UnityEngine;
using System;
using System.Collections;

public class HUDScript : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private ShopPage shopPage;

    private void Start()
    {
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

    public void OpenShop()
    {
        shopPage.Show();
    }

    public void CloseShop()
    {
        shopPage.Hide();
    }
}