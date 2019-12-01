using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    Player player;
    Inventory playersInventory;
    TextMeshProUGUI moneyText;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public float totalHearts = 3;

    private void Start()
    {
        moneyText = GameObject.Find("CurrencyText").GetComponent<TextMeshProUGUI>();

        player = FindObjectOfType<Player>();
        player.DamageTaken += UpdateHearts;
        player.HealthGiven += UpdateHearts;
        playersInventory = player.GetComponent<Inventory>();
        playersInventory.MoneyChanged += UpdateMoney;
        InitHearts();
        UpdateHearts();
        UpdateMoney();
    }

    void InitHearts()
    {
        for (int i = 0; i < totalHearts; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    void UpdateHearts()
    {
        float tempHealth = player.currHealth / 2;
        for (int currentHeart = 0; currentHeart < totalHearts; currentHeart++)
        {
            if(currentHeart <= tempHealth - 1)
            {
                hearts[currentHeart].sprite = fullHeart;
            }
            else if(currentHeart >= tempHealth)
            {
                hearts[currentHeart].sprite = emptyHeart;
            }
            else
            {
                hearts[currentHeart].sprite = halfHeart;
            }
        }
    }

    void UpdateMoney()
    {
        moneyText.text = playersInventory.money.ToString("0000");
    }
}
