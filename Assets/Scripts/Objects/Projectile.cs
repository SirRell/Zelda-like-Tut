using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IDamageable
{
    public float damage = 1;
    public float moveSpeed = 1;
    public float lifetime = 5;
    public float torque;
    protected float lifetimeTimer;
    protected Rigidbody2D rb;
    public ParticleSystem particles;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lifetimeTimer = lifetime;
        rb.AddForce(transform.right.normalized * moveSpeed, ForceMode2D.Impulse);
        rb.AddTorque(torque);
    }

    void Update()
    {
        lifetimeTimer -= Time.deltaTime;
        if(lifetimeTimer <= 0 && gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage, gameObject);
            if (other.gameObject.GetComponent<Projectile>())
                return;
            else
                Destroy();
        }
        else
        {
            Destroy();
        }
    }

    public virtual void TakeDamage(float damageTaken, GameObject damageGiver)
    {
        Destroy();
        //throw new System.NotImplementedException();
    }

    public void Destroy()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        Destroy(gameObject, 2);
    }
}
