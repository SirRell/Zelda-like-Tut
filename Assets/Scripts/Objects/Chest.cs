using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    bool isOpen;
    private void Update()
    {
        if (playerInRange && Input.GetButtonDown("Submit"))
        {
            if (!isOpen)
            {
                Interacting();
                GetComponent<Animator>().enabled = true;
                isOpen = true;
            }

        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(!isOpen)
            base.OnTriggerEnter2D(other);
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }
}
