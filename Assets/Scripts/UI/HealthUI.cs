using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    Player player;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public float totalHearts = 3;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        player.HealthChanged += UpdateHearts;
        InitHearts();
        UpdateHearts();
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
}
