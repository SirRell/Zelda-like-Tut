using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum ItemType
{   CommonKey,
    UncommonKey,
    BossKey,
    Heart,
    Money,
    Bomb,
    Stick,
    Arrow,
    MagicBottle,
    HeartContainer
}

public class Item : MonoBehaviour
{

    public Sprite contextImage;
    public AudioClip pickupSound;
    public float jumpStrength = 1;
    public int jumps = 1;

    [Tooltip("Always 'CollectableContext' Prefab")]
    public GameObject itemDisplay;
    public ItemType type;
    public string itemName;
    [Multiline]
    public string itemDescription;
    public int value = 1;


    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(DisplayItem(other));
    }

    IEnumerator DisplayItem(Collider2D other)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        GameObject display = Instantiate(itemDisplay, other.transform.position + Vector3.up * 1.5f, transform.rotation, other.transform);
        display.GetComponent<SpriteRenderer>().sprite = contextImage;
        Collect(other.GetComponent<Inventory>());

        //Make the item display bounce a little
        display.transform.DOLocalJump(Vector3.zero, jumpStrength, jumps, 1);

        yield return new WaitForSeconds(1f);
        Destroy(display);
        Destroy(gameObject);
    }

    public virtual void Collect(Inventory playerInventory)
    {
        playerInventory.ReceiveItem(gameObject, value);
    }
}
