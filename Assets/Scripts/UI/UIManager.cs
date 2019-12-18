using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    Player player;
    Inventory playersInventory;
    public TextMeshProUGUI moneyText;
    public Slider magicBar;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public float totalHearts = 3;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        player.DamageTaken += UpdateHearts;
        player.HealthGiven += UpdateHearts;
        playersInventory = player.GetComponent<Inventory>();
        playersInventory.MoneyChanged += UpdateMoney;
        playersInventory.AmmoChanged += UpdateAmmo;
        playersInventory.MagicChanged += UpdateMagic;
        InitHearts();
        UpdateHearts();
        UpdateMoney();
        InitMagic();
        UpdateMagic();
    }

    void InitHearts()
    {
        for (int i = 0; i < totalHearts; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    void InitMagic()
    {
        magicBar.maxValue = playersInventory.maxAmmo;
        magicBar.value = playersInventory.currentAmmo;
    }

    void InitAmmo()
    {
        Debug.LogError("This method has not been implemented yet");
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
        if(moneyText != null)
            moneyText.text = playersInventory.money.ToString("0000");
    }

    void UpdateAmmo()
    {
        if (magicBar != null)
            magicBar.value = playersInventory.currentAmmo;
    }

    void UpdateMagic()
    {
        if(magicBar != null)
            magicBar.value = playersInventory.currentAmmo;
    }
}
