using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public GameObject contents;
    public Sprite openSprite;
    public bool isOpen;
    string uniqueID;

    override protected void Start()
    {
        base.Start();

        uniqueID = UnityEngine.SceneManagement.SceneManager.GetActiveScene() + name + transform.position;
        if (InfoManager.Instance.chests.TryGetValue(uniqueID, out bool temp))
        {
            isOpen = temp;
            if (isOpen)
            {
                GetComponent<SpriteRenderer>().sprite = openSprite;
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        else
        {
            InfoManager.Instance.chests.Add(uniqueID, isOpen);
        }
    }

    protected override void Update()
    {
        if (playerInRange && Input.GetButtonDown("Submit"))
        {
            if (!isOpen)
            {
                //***BUG*** Multiple chests can be opened at the same time
                Interacting();
            }

        }
    }

    protected override void Interacting()
    {
        base.Interacting();
        GetComponent<BoxCollider2D>().enabled = false;

        GetComponent<Animator>().enabled = true;
        isOpen = true;
        InfoManager.Instance.chests[uniqueID] = isOpen;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(!isOpen)
            base.OnTriggerEnter2D(other);
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }

    public void GiveItem()
    {
        if (contents == null)
        {
            contents = gameObject.GetComponent<Collectables>().GetRandomItem();
        }

        player.GetComponent<Inventory>().ReceiveChestItem(contents);
        GetComponent<Animator>().enabled = false;
        //contents = null;
    }
}
