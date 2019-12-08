using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1;
    public float moveSpeed = 1;
    public float lifetime = 5;
    public float torque;
    float lifetimeTimer;
    //ParticleSystem particles;


    // Start is called before the first frame update
    void Start()
    {
        lifetimeTimer = lifetime;
        GetComponent<Rigidbody2D>().AddForce(transform.right * moveSpeed, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().AddTorque(torque);
    }

    // Update is called once per frame
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
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage, gameObject);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Destroy()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        //Instantiate(particles, transform.position, Quaternion.identity);
    }
}
