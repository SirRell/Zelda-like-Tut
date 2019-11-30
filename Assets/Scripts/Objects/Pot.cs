using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour, IDamageable
{
    Animator anim;
    public GameObject contents;

    void Start()
    {
        anim = GetComponent<Animator>();
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
    }

    public void SpawnItem(GameObject contents)
    {
        Instantiate(contents, transform.position, Quaternion.identity);
    }

    void Disable()
    {
        anim.SetBool("Destroyed", false);
        gameObject.SetActive(false);
    }
}
