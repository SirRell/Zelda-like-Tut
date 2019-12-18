using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    readonly float thrust = 8.5f;
    Rigidbody2D myRB;

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    public IEnumerator KnockBack(Transform otherTransform)
    {
        Vector2 forceDirection = transform.position - otherTransform.position;
        Vector2 force = forceDirection.normalized * thrust;
        myRB.velocity = force;
        yield return new WaitForSeconds(.15f);
        myRB.velocity = Vector2.zero;
        if(transform.CompareTag("Enemy"))
            GetComponent<Enemy>().ChangeState(EnemyState.Chase);
        if (transform.CompareTag("Player"))
            GetComponent<Player>().ChangeState(PlayerState.Walk);
    }
}
