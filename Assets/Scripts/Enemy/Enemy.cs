using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable<float>, IDestroyable
{
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed = 5f;

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
        if (health <= 0f)
        {
            Destroy();
        }
        else
        {
            Knockback kB = GetComponent<Knockback>();
            if (kB!= null)
            {
                kB.StartCoroutine(kB.KnockBack(kB.otherTransform));
            }
        }
    }

    protected virtual void Start()
    {
        
    }
}
