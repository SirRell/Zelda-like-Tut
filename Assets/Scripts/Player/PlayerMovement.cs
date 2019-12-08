using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Player
{
    public float speed = 4f;
    Vector2 moveDir;
    Animator anim;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        anim.SetFloat("moveX", 0f);
        anim.SetFloat("moveY", -1);
    }

    private void Update()
    {
        moveDir = Vector2.zero;
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("Attack") && currentState != PlayerState.Stagger)
        {
            if(currentState != PlayerState.Attack && currentState != PlayerState.Interact)
                StartCoroutine(AttackCo());
        }
        else if(currentState == PlayerState.Walk || currentState == PlayerState.Interact)
        {
            UpdateAnimation();
        }
    }

    IEnumerator AttackCo()
    {
        anim.SetBool("attacking", true);
        ChangeState(PlayerState.Attack);
        //sounds.PlayClip(sounds.swordSwing);
        SoundsManager.instance.PlayClip(SoundsManager.Sound.PlayerSwordSwing);
        yield return null;
        anim.SetBool("attacking", false);
        yield return new WaitForSeconds(.33f);
        ChangeState(PlayerState.Walk);
    }

    void UpdateAnimation()
    {
        if (moveDir != Vector2.zero)
        {
            MoveCharacter();
            anim.SetBool("moving", true);
            anim.SetFloat("moveX", moveDir.x);
            anim.SetFloat("moveY", moveDir.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        rb.MovePosition((Vector2)transform.position + moveDir.normalized * speed * Time.deltaTime);
    }

    //When attacking
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(currentState == PlayerState.Attack)
        {
            if(other.GetComponent<IDamageable>() != null)
            {
                other.GetComponent<IDamageable>().TakeDamage(strength, gameObject);
                //sounds.PlayClip(sounds.swordHit);
                SoundsManager.instance.PlayClip(SoundsManager.Sound.PlayerSwordHit);
            }
        }
    }

}
