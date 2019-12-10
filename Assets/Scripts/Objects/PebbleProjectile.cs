using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PebbleProjectile : Projectile
{
    public override void TakeDamage(float damageTaken, GameObject damageGiver)
    {
        GetComponent<Rigidbody2D>().velocity =  -GetComponent<Rigidbody2D>().velocity;
        lifetimeTimer = lifetime;
    }
}
