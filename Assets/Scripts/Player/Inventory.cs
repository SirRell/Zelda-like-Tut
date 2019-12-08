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
    public event Action MoneyChanged;
    

    private void Start()
    {
        context = GetComponentInChildren<ContextClue>();
        MyItems = InfoManager.Instance.items;
        commonKeys = InfoManager.Instance.CommonKeys;
        uncommonKeys = InfoManager.Instance.UnCommonKeys;
        bossKeys = InfoManager.Instance.BossKeys;
        money = InfoManager.Instance.Money;

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

    public void ReceiveItem(GameObject itemToReceiveGO)
    {
        Item itemToReceive = itemToReceiveGO.GetComponent<Item>();
        switch (itemToReceive.type)
        {
            case ItemType.CommonKey:
                commonKeys++;
                break;
            case ItemType.UncommonKey:
                uncommonKeys++;
                break;
            case ItemType.BossKey:
                bossKeys++;
                break;
            case ItemType.Heart:
                itemToReceive.Collect(this);
                break;
            case ItemType.Money:
                money += itemToReceiveGO.GetComponent<Coin>().coinValue;
                MoneyChanged?.Invoke();
                break;
            case ItemType.Bomb:
                break;
            case ItemType.Stick:
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
