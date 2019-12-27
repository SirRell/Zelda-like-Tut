using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    Player player;
    Inventory playersInventory;
    public GameObject heartContainer;
    public GameObject pausePanel;
    public TextMeshProUGUI moneyText;
    public Slider magicBar;

    public List<Image> hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        player.DamageTaken += UpdateHearts;
        player.HealthGiven += UpdateHearts;
        playersInventory = player.GetComponent<Inventory>();
        playersInventory.MoneyChanged += UpdateMoney;
        playersInventory.AmmoChanged += UpdateAmmo;
        playersInventory.MagicChanged += UpdateMagic;
        playersInventory.HeartAmountChanged += UpdateHeartContainer;
        InitHearts();
        UpdateHearts();
        UpdateMoney();
        InitMagic();
        UpdateMagic();
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!pausePanel.activeInHierarchy)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }
    }

    void InitHearts()
    {
        int maximumHearts = (int)player.maxHealth / 2;
        for (int i = 0; i < maximumHearts; i++)
        {
            if(i >= hearts.Count)
            {
                GameObject heartImage = Instantiate(new GameObject("HeartImage"), heartContainer.transform);
                Image image = heartImage.AddComponent<Image>();
                image.sprite = fullHeart;
                RectTransform t = heartImage.GetComponent<RectTransform>();
                t.sizeDelta = new Vector2(30, 30); //Width & Height of the RectTransform
                hearts.Add(image);
            }
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
        for (int currentHeart = 0; currentHeart < hearts.Count; currentHeart++)
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

    void UpdateHeartContainer()
    {
        InitHearts();
        UpdateHearts();
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

    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void Quit()
    {
        print("Quit Game");
    }
}
