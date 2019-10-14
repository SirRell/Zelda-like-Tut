using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEnemy : Enemy
{
    Rigidbody2D rb;
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
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        float distFromTarget = Vector2.Distance(target.position, transform.position);
        if (distFromTarget <= chaseRadius && distFromTarget > attackRadius)
        {
            if (currentState != EnemyState.Stagger)
            {
                Vector2 temp = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                rb.MovePosition(temp);
            }
        }
        else if (distFromTarget > chaseRadius)
        {
            if(currentState != EnemyState.Stagger)
            {
                Vector2 temp = Vector2.MoveTowards(transform.position, homePosition, moveSpeed * Time.deltaTime);
                rb.MovePosition(temp);
            }
        }
    }
}
