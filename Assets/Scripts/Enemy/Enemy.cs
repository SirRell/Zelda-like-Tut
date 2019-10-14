﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable<float>
{
    public enum EnemyState
    {
        Idle,
        Walk,
        Attack,
        Stagger
    }

    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed = 5f;
    protected EnemyState currentState;

    protected virtual void Start()
    {
        ChangeState(EnemyState.Idle);
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
            if (kB!= null)
            {
                StartCoroutine(kB.KnockBack(kB.otherTransform));
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