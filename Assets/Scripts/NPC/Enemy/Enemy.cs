using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack,
        Stagger,
        Patrol
    }
    
    public float maxHealth = 2f;
    public float currHealth;
    //public string enemyName;
    public int baseAttack;
    public float moveSpeed = 2f;
    protected EnemyState currentState;
    protected Rigidbody2D rb;
    protected Animator anim;
    public GameObject deathFX;
    protected Patrol patroller;
    protected FireProjectiles shooter;


    protected virtual void Awake()
    {
        ChangeState(EnemyState.Idle);
        currHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        patroller = GetComponent<Patrol>();
        shooter = GetComponent<FireProjectiles>();
    }

    protected virtual void OnEnable()
    {
        currHealth = maxHealth;
    }

    public virtual void Destroy()
    {
        gameObject.SetActive(false);
        GameObject deathEffect = Instantiate(deathFX, transform.position, Quaternion.identity);
        Destroy(deathEffect, 1f);
    }

    public void TakeDamage(float damageTaken, GameObject damageGiver)
    {
        currHealth -= damageTaken;
        if (currHealth <= 0f)
        {
            Destroy();
        }
        else
        {
            Knockback kB = GetComponent<Knockback>();
            if (kB!= null)
            {
                ChangeState(EnemyState.Stagger);
                StartCoroutine(kB.KnockBack(damageGiver.transform));
            }
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
            currentState = newState;
    }

}
