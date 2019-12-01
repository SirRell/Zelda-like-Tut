using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Inventory : MonoBehaviour
{
    ContextClue context;
    public GameObject dialogueBox;
    TextMeshProUGUI dialogueText;
    public List<Items> MyItems;
    public int commonKeys;
    public int uncommonKeys;
    public int bossKeys;
    public int money;
    bool receivingItem;
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
        if(receivingItem && Input.GetButtonDown("Submit"))
        {
            receivingItem = false;
            GetComponent<Animator>().SetBool("receiveItem", false);
            GetComponent<PlayerMovement>().enabled = true;
            context.StopInteracting();
            dialogueBox.SetActive(false);
        }
    }

    public void ReceiveItem(Items itemToReceive)
    {
        receivingItem = true;

        dialogueText.text = "You found a " + itemToReceive.itemName + "\n" + itemToReceive.itemDescription;
        dialogueBox.SetActive(true);

        if (itemToReceive.isKey)
        {
            commonKeys++;
        }
        MyItems.Add(itemToReceive);
        context.GetComponent<SpriteRenderer>().sprite = itemToReceive.itemDisplay;
        Animate();
    }

    public void ReceiveCollectable(Collectable collectableToReceive)
    {
        switch (collectableToReceive.type)
        {
            case CollectableType.Heart:
                break;
            case CollectableType.Money:
                money += collectableToReceive.GetComponentInParent<Coin>().coinValue;
                MoneyChanged?.Invoke();
                break;
            case CollectableType.Bomb:
                break;
            case CollectableType.Stick:
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
    }
}
