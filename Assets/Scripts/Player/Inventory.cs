using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    ContextClue context;
    public List<Items> MyItems;
    public int commonKeys;
    public int uncommonKeys;
    public int bossKeys;
    bool receivingItem;

    private void Start()
    {
        context = GetComponentInChildren<ContextClue>();
        MyItems = InfoManager.Instance.items;
        commonKeys = InfoManager.Instance.Keys;
    }

    private void Update()
    {
        if(receivingItem && Input.GetButtonDown("Submit"))
        {
            receivingItem = false;
            GetComponent<Animator>().SetBool("receiveItem", false);
            GetComponent<PlayerMovement>().enabled = true;
            context.StopInteracting();
        }
    }

    public void ReceiveItem(Items itemToReceive)
    {
        receivingItem = true;

        if (itemToReceive.isKey)
        {
            commonKeys++;
        }
        MyItems.Add(itemToReceive);
        context.GetComponent<SpriteRenderer>().sprite = itemToReceive.itemDisplay;
        Animate();
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
