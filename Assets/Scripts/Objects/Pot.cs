using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour, IDamageable
{
    Animator anim;
    public GameObject contents;
    Collectables newItem;

    void Start()
    {
        anim = GetComponent<Animator>();
        newItem = GetComponent<Collectables>();
    }

    public void TakeDamage(float damageTaken, GameObject damageGiver)
    {
        Destroy();
        anim.enabled = true;
    }

    public void Destroy()
    {
        anim.SetBool("Destroyed", true);
        Invoke("Disable", 2f);

        if(contents != null)
            SpawnItem(contents);
        else
        {
            GameObject droppedItem = newItem.GetRandomItem();
            if(droppedItem != null)
                SpawnItem(droppedItem);
        }

    }

    void SpawnItem(GameObject contents)
    {
        Instantiate(contents, transform.position, contents.transform.rotation);
    }

    void Disable()
    {
        anim.SetBool("Destroyed", false);
        gameObject.SetActive(false);
    }
}
