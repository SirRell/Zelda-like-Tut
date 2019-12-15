using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Enemy
{
    Transform target;

    public float chaseRadius;
    public float attackRadius;
    public AudioClip wakeUpSound;
    Vector2 homePosition;
    bool playerInRange = false;

    AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        homePosition = transform.position;
        target = GameObject.FindWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        GetComponentInChildren<CircleCollider2D>().radius = chaseRadius - .15f;

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        playerInRange = false;

        if (moveSpeed == 0)
            anim.SetBool("stationary", true);

        if ((Vector2)transform.position != homePosition)
            transform.position = homePosition;
        StartCoroutine(CheckDistance());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }

    }

    IEnumerator CheckDistance()
    {
        while (true)
        {
            if (playerInRange)
            {
                float distFromTarget = Vector2.Distance(target.position, transform.position);
                // Within chase radius but not within attack radius, and target is still alive
                if (distFromTarget <= chaseRadius && distFromTarget > attackRadius && target.gameObject.activeInHierarchy)
                {
                    if (patroller != null)
                    {
                        patroller.patroling = false;
                    }

                    if (currentState == EnemyState.Idle)
                    {
                        float delay = 0;
                        anim.SetTrigger("Wakeup");
                        if (wakeUpSound != null)
                            audioSource.PlayOneShot(wakeUpSound);
                        yield return new WaitForEndOfFrame();
                        AnimatorClipInfo[] clips = anim.GetCurrentAnimatorClipInfo(0);
                        delay = clips[0].clip.length;
                        yield return new WaitForSeconds(delay);
                        ChangeState(EnemyState.Chase);
                    }
                    else if (currentState == EnemyState.Patrol)
                    {
                        ChangeState(EnemyState.Chase);
                    }
                    else if (currentState == EnemyState.Chase)
                    {
                        Vector2 temp = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                        UpdateAnimation((temp - (Vector2)transform.position).normalized);
                        rb.MovePosition(temp);
                    }
                }
                else if (distFromTarget <= attackRadius)
                {
                    if (shooter != null)
                    {
                        shooter.firing = true;
                        ChangeState(EnemyState.Attack);
                        UpdateAnimation((target.position - transform.position).normalized);
                    }
                    else if (currentState == EnemyState.Chase)
                    {
                        ChangeState(EnemyState.Attack);
                        StartCoroutine(Attack());
                    }
                }


            }
            // Far from target, or target is dead
            if (!playerInRange || !target.gameObject.activeInHierarchy)
            {
                if (currentState != EnemyState.Stagger)
                {
                    if (patroller != null)
                    {
                        patroller.patroling = true;
                        if (currentState == EnemyState.Idle)
                        {
                            anim.SetTrigger("Wakeup");
                            yield return new WaitForSeconds(.5f);
                        }
                        ChangeState(EnemyState.Patrol);
                        yield return null;
                    }

                    if (shooter != null)
                    {
                        shooter.firing = false;
                    }

                    Vector2 temp = Vector2.MoveTowards(transform.position, homePosition, moveSpeed * Time.deltaTime);
                    rb.MovePosition(temp);

                    if ((Vector2)transform.position == homePosition && currentState != EnemyState.Idle)
                    {
                        anim.SetTrigger("Sleep");
                        ChangeState(EnemyState.Idle);
                        yield return null;
                    }
                    UpdateAnimation((temp - (Vector2)transform.position).normalized);
                }
            }
            // Within Attack Radius
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator Attack()
    {
        float delay = 0;
        Vector2 temp = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        anim.SetTrigger("Attack");
        target.GetComponent<IDamageable>().TakeDamage(baseAttack, gameObject);
        yield return new WaitForEndOfFrame();
        AnimatorClipInfo[] clips = anim.GetCurrentAnimatorClipInfo(0);
        delay = clips[0].clip.length;
        yield return new WaitForSeconds(Mathf.Max(.5f, delay));
        ChangeState(EnemyState.Chase);
        UpdateAnimation((temp - (Vector2)transform.position).normalized);
    }

    void UpdateAnimation(Vector2 direction)
    {
        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
    }
}
