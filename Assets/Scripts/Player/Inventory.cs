using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Inventory : MonoBehaviour
{
    ContextClue context;
    GameObject itemToShow;
    TextMeshProUGUI dialogueText;
    bool receivedItem;

    public GameObject dialogueBox;
    public List<Item> MyItems;
    public int commonKeys;
    public int uncommonKeys;
    public int bossKeys;
    public int money;
    public int currentAmmo, maxAmmo = 10;
    public event Action MoneyChanged;
    public event Action AmmoChanged;
    public event Action MagicChanged;
    public event Action HeartAmountChanged;


    private void Start()
    {
        context = GetComponentInChildren<ContextClue>();
        MyItems = InfoManager.Instance.items;
        commonKeys = InfoManager.Instance.CommonKeys;
        uncommonKeys = InfoManager.Instance.UnCommonKeys;
        bossKeys = InfoManager.Instance.BossKeys;
        money = InfoManager.Instance.Money;
        currentAmmo = InfoManager.Instance.AmmoLeft;
        //dialogueBox = GameObject.Find("Dialogue Box");
        dialogueText = dialogueBox.GetComponentInChildren<TextMeshProUGUI>();

    }

    private void Update()
    {
        if(receivedItem && Input.GetButtonDown("Submit"))
        {
            receivedItem = false;
            GetComponent<Animator>().SetBool("receiveItem", false);
            GetComponent<PlayerMovement>().enabled = true;
            context.StopInteracting();
            dialogueBox.SetActive(false);
        }
    }

    public void ReceiveChestItem(GameObject itemToReceiveGO)
    {
        Item itemToReceive = itemToReceiveGO.GetComponent<Item>();

        dialogueText.text = "You found a " + itemToReceive.itemName + "\n" + itemToReceive.itemDescription;

        itemToShow = itemToReceiveGO;

        MyItems.Add(itemToReceive);
        context.GetComponent<SpriteRenderer>().sprite = itemToReceive.contextImage;
        Animate();
    }

    public void ReceiveItem(GameObject itemToReceiveGO, int amount = 1)
    {
        Item itemToReceive = itemToReceiveGO.GetComponent<Item>();
        switch (itemToReceive.type)
        {
            case ItemType.CommonKey:
                commonKeys += amount;
                break;
            case ItemType.UncommonKey:
                uncommonKeys += amount;
                break;
            case ItemType.BossKey:
                bossKeys += amount;
                break;
            case ItemType.Heart:
                Player player = GetComponent<Player>();
                player.Heal(amount);
                break;
            case ItemType.Money:
                money += amount;
                MoneyChanged?.Invoke();
                break;
            case ItemType.Bomb:
                break;
            case ItemType.Stick:
                break;
            case ItemType.Arrow:
                currentAmmo += amount;
                AmmoChanged?.Invoke();
                break;
            case ItemType.MagicBottle:
                MagicChanged?.Invoke();
                break;
            case ItemType.HeartContainer:
                GetComponent<Player>().maxHealth += amount;
                HeartAmountChanged?.Invoke();
                break;
            default:
                break;
        }
        if(itemToReceive.pickupSound != null)
        {
            SoundsManager.instance.GetComponent<AudioSource>().PlayOneShot(itemToReceive.pickupSound);
        }
    }

    public void RemoveItem(ItemType itemToRemove, int amount = 1)
    {
        switch (itemToRemove)
        {
            case ItemType.CommonKey:
                commonKeys-= amount;
                break;
            case ItemType.UncommonKey:
                uncommonKeys -= amount;
                break;
            case ItemType.BossKey:
                bossKeys -= amount;
                break;
            case ItemType.Heart:
                break;
            case ItemType.Money:
                money -= amount;
                MoneyChanged?.Invoke();
                break;
            case ItemType.Bomb:
                break;
            case ItemType.Stick:
                break;
            case ItemType.Arrow:
                currentAmmo -= amount;
                AmmoChanged?.Invoke();
                break;
            case ItemType.HeartContainer:
                break;
            default:
                break;
        }
    }

    void Animate()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("receiveItem", true);
        GetComponent<PlayerMovement>().enabled = false;
    }

    public void ShowItem()
    {
        context.GetComponent<SpriteRenderer>().enabled = true;
        ReceiveItem(itemToShow);
        dialogueBox.SetActive(true);
        receivedItem = true;
    }
}
