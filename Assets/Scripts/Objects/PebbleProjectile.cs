using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PebbleProjectile : Projectile
{
    Vector2 currVelocity;

    protected override void Start()
    {
        base.Start();
        currVelocity = rb.velocity;
    }

    public override void TakeDamage(float damageTaken, GameObject damageGiver)
    {
        GetComponent<Rigidbody2D>().velocity =  -currVelocity;
        lifetimeTimer = lifetime;
    }
}
