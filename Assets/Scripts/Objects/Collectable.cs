using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum CollectableType
{   Heart,
    Money,
    Bomb,
    Stick
}

public class Collectable : MonoBehaviour
{
    public CollectableType type;
    public Sprite contextImage;
    public GameObject itemDisplay;
    public float jumpStrength;
    public int jumps;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(DisplayCollectable(other));
    }

    IEnumerator DisplayCollectable(Collider2D other)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        GameObject display = Instantiate(itemDisplay, other.transform.position + Vector3.up * 1.5f, Quaternion.identity, other.transform);
        display.GetComponent<SpriteRenderer>().sprite = contextImage;

        Collect(other);
        //Make the item display bounce a little
        display.transform.DOLocalJump(Vector3.zero, jumpStrength, jumps, 1);

        yield return new WaitForSeconds(1f);
        Destroy(display);
        Destroy(gameObject);
    }

    public virtual void Collect(Collider2D other)
    {

    }
}
