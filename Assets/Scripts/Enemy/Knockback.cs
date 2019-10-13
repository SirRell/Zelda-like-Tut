using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    Rigidbody2D myRB;
    public Transform otherTransform;

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        otherTransform = other.GetComponent<Transform>();
    }

    public IEnumerator KnockBack(Transform otherTransform)
    {
        Vector2 forceDirection = transform.position - otherTransform.parent.position;
        Vector2 force = forceDirection.normalized * thrust;

        myRB.velocity = force;
        yield return new WaitForSeconds(.15f);
        myRB.velocity = Vector2.zero;
    }
}
