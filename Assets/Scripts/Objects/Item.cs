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
    Stick
}

public class Item : MonoBehaviour
{
    public Sprite contextImage;
    public AudioClip pickupSound;
    public float jumpStrength = 1;
    public int jumps = 1;

    public GameObject itemDisplay;
    public ItemType type;
    public string itemName, itemDescription;


    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(DisplayItem(other));
    }

    IEnumerator DisplayItem(Collider2D other)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        GameObject display = Instantiate(itemDisplay, other.transform.position + Vector3.up * 1.5f, Quaternion.identity, other.transform);
        display.GetComponent<SpriteRenderer>().sprite = contextImage;
        display.GetComponent<AudioSource>().PlayOneShot(pickupSound);
        Collect(other.GetComponent<Inventory>());

        //Make the item display bounce a little
        display.transform.DOLocalJump(Vector3.zero, jumpStrength, jumps, 1);

        yield return new WaitForSeconds(1f);
        Destroy(display);
        Destroy(gameObject);
    }

    public virtual void Collect(Inventory playerInventory)
    {

    }
}
