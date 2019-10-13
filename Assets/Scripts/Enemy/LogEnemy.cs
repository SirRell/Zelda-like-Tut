using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEnemy : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Vector2 homePosition;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        homePosition = transform.position;
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius &&
            Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("LogEnemy was hit by player");
    }
}
