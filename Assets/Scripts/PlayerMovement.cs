using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Walk,
    Attack,
    Interact
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed = 4f;
    Rigidbody2D rb;
    Vector2 moveDir;
    Animator anim;

    private void Start()
    {
        currentState = PlayerState.Walk;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //anim.SetFloat("moveX", 0f);
        anim.SetFloat("moveY", -1);
    }

    private void Update()
    {
        moveDir = Vector2.zero;
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("Attack") && currentState == PlayerState.Walk)
        {
            StartCoroutine(AttackCo());
        }
        else if(currentState == PlayerState.Walk)
        {
            UpdateAnimation();
        }
    }

    IEnumerator AttackCo()
    {
        anim.SetBool("attacking", true);
        currentState = PlayerState.Attack;
        yield return null;
        anim.SetBool("attacking", false);
        yield return new WaitForSeconds(.33f);
        currentState = PlayerState.Walk;
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
            anim.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        rb.MovePosition((Vector2)transform.position + moveDir.normalized * speed * Time.deltaTime);
    }
}
