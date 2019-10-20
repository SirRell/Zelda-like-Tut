using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable<float, Player>
{
    public enum EnemyState
    {
        Idle,
        Walk,
        Attack,
        Stagger
    }
    
    public float maxHealth = 2f;
    public float currHealth;
    //public string enemyName;
    public int baseAttack;
    public float moveSpeed = 5f;
    protected EnemyState currentState;
    protected Rigidbody2D rb;
    protected Animator anim;

    protected virtual void Awake()
    {
        ChangeState(EnemyState.Idle);
        currHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    protected virtual void OnEnable()
    {
        currHealth = maxHealth;
    }

    public virtual void Destroy()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damageTaken, Player damageGiver)
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
                StartCoroutine(kB.KnockBack(damageGiver.gameObject.transform));
                ChangeState(EnemyState.Stagger);
            }
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
            currentState = newState;
    }

}
