using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable<float, Enemy>
{
    public enum PlayerState
    {
        Walk,
        Attack,
        Interact,
        Stagger
    }

    protected Rigidbody2D rb;
    public PlayerState currentState;
    public float maxHealth = 5f;
    public float currHealth;
    public float strength = 1f;


    protected virtual void Start()
    {
        ChangeState(PlayerState.Walk);
        rb = GetComponent<Rigidbody2D>();
        currHealth = maxHealth;

    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damageTaken, Enemy damageGiver)
    {
        currHealth -= damageTaken;
        if (currHealth <= 0f)
        {
            Destroy();
        }
        else
        {
            Knockback kB = GetComponent<Knockback>();
            if (kB != null)
            {
                StartCoroutine(kB.KnockBack(damageGiver.gameObject.transform));
                GetComponent<PlayerMovement>().ChangeState(PlayerState.Stagger);
            }
        }
    }

    public void ChangeState(PlayerState newState)
    {
        if (currentState != newState)
            currentState = newState;
    }

}
