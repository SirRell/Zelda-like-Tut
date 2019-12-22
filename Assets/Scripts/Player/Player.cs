using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum PlayerState
{
    Walk,
    Attack,
    Interact,
    Stagger
}

public class Player : MonoBehaviour, IDamageable
{
    protected Rigidbody2D rb;
    public PlayerState currentState;
    public float maxHealth = 6f;
    public float currHealth;
    public float strength = 1f;
    public event Action DamageTaken;
    public event Action HealthGiven;
    public GameObject deathFX;

    protected virtual void Start()
    {
        ChangeState(PlayerState.Walk);
        rb = GetComponent<Rigidbody2D>();

        if (InfoManager.Instance.PlayerHealth != 0)
            currHealth = InfoManager.Instance.PlayerHealth;
        else
            currHealth = maxHealth;

        if (InfoManager.Instance.PlayerMaxHealth != 0)
            maxHealth = InfoManager.Instance.PlayerMaxHealth;

        if (InfoManager.Instance.NewPlayerPosition != Vector2.zero)
            transform.position = InfoManager.Instance.NewPlayerPosition;
    }

    public void Destroy()
    {
        GameObject deathEffects;
        if(deathFX != null)
            deathEffects = Instantiate(deathFX, transform.position, Quaternion.identity);
        gameObject.SetActive(false);        
    }

    public void TakeDamage(float damageTaken, GameObject damageGiver)
    {
        currHealth -= damageTaken;
        SoundsManager.instance.PlayClip(SoundsManager.Sound.PlayerDamaged);
        DamageTaken?.Invoke();
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
        //Find out if the max health is even or not using modulo operator
        if (maxHealth%2 == 0) //Is even, because something divided by two without remainder is even.
        {
            currHealth = Mathf.Clamp(currHealth, 0, maxHealth);
        }
        else
        {
            //Is odd. The heart isn't showing, so it shouldn't be filled
            currHealth = Mathf.Clamp(currHealth, 0, maxHealth - 1); 
        }

        HealthGiven?.Invoke();
    }

    public void ChangeState(PlayerState newState)
    {
        if (currentState != newState)
            currentState = newState;
    }
}