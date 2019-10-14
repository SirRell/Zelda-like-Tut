using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEnemy : Enemy
{
    Transform target;
    public float chaseRadius;
    public float attackRadius;
    Vector2 homePosition;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        homePosition = transform.position;
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (target.gameObject.activeInHierarchy)
            CheckDistance();
    }

    void CheckDistance()
    {
        float distFromTarget = Vector2.Distance(target.position, transform.position);
        if (distFromTarget <= chaseRadius && distFromTarget > attackRadius)
        {
            if(currentState == EnemyState.Idle)
            {
                anim.SetTrigger("Wakeup");
                ChangeState(EnemyState.Walk);
            }
            else if (currentState == EnemyState.Walk)
            {
                Vector2 temp = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                UpdateAnimation((temp - (Vector2)transform.position).normalized);
                rb.MovePosition(temp);
            }
        }
        else if(distFromTarget < attackRadius)
        {
            if(currentState == EnemyState.Walk)
            {
                ChangeState(EnemyState.Attack);
                target.GetComponent<IDamageable<float, Enemy>>().TakeDamage(baseAttack, GetComponent<Enemy>());
                ChangeState(EnemyState.Walk);
            }
        }
        else if (distFromTarget > chaseRadius)
        {
            if(currentState != EnemyState.Stagger)
            {
                Vector2 temp = Vector2.MoveTowards(transform.position, homePosition, moveSpeed * Time.deltaTime);
                rb.MovePosition(temp);

                if ((Vector2)transform.position == homePosition && currentState != EnemyState.Idle)
                {
                    anim.SetTrigger("Sleep");
                    ChangeState(EnemyState.Idle);
                    return;
                }
                UpdateAnimation((temp - (Vector2)transform.position).normalized);
            }
        }


    }

    void UpdateAnimation(Vector2 direction)
    {
        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);

        //if(target.gameObject.activeInHierarchy && currentState != EnemyState.Idle)
        //{
        //    anim.SetTrigger("Sleep");
        //    ChangeState(EnemyState.Idle);
        //}
    }
}
