using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable<float>
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
    public float health = 5f;
    public float strength = 1f;


    protected virtual void Start()
    {
        ChangeState(PlayerState.Walk);
        rb = GetComponent<Rigidbody2D>();

    }

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
            if (kB != null)
            {
                StartCoroutine(kB.KnockBack(kB.otherTransform));
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
