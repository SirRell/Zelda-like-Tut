using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IDamageable
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
    public event Action HealthChanged;

    protected virtual void Start()
    {
        ChangeState(PlayerState.Walk);
        rb = GetComponent<Rigidbody2D>();

        if (InfoManager.Instance.PlayerHealth != 0)
            currHealth = InfoManager.Instance.PlayerHealth;
        else
            currHealth = maxHealth;

        if(InfoManager.Instance.NewPlayerPosition != Vector2.zero)
            transform.position = InfoManager.Instance.NewPlayerPosition;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damageTaken, GameObject damageGiver)
    {
        currHealth -= damageTaken;
        HealthChanged?.Invoke();
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

    public void Heal(int healthGiven)
    {
        currHealth += healthGiven;
        currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
        HealthChanged?.Invoke();
    }

    public void ChangeState(PlayerState newState)
    {
        if (currentState != newState)
            currentState = newState;
    }
}
