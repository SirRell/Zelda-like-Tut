using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    /* *If more than the player needs to know something
     * is being interacted with, Actions will be reimplemented*/
    //public event System.Action<Sprite> MeInteractable;
    //public event System.Action NotInteracting;
    //public event System.Action Interacting;

    public Sprite contextImage;
    protected bool playerInRange = false;
    ContextClue playerCC;
    

    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        playerInRange = true;
        playerCC = other.gameObject.GetComponentInChildren<ContextClue>();
        MeInteractable();
    }

    virtual protected void Interacting()
    {
        if (playerCC != null)
            playerCC.Interacting();
        GetComponent<BoxCollider2D>().enabled = false;
        //Interacting?.Invoke();
    }

    virtual protected void MeInteractable()
    {
       if(playerCC != null)
            playerCC.Interactable(contextImage);
        //MeInteractable?.Invoke(contextImage);
    }

    virtual protected void StopInteracting()
    {
        if (playerCC != null)
            playerCC.StopInteracting();
        //NotInteracting?.Invoke();
    }
    public ContactFilter2D filter;
    virtual protected void OnTriggerExit2D(Collider2D other)
    {
        //BoxCollider2D other.GetComponent<BoxCollider2D>();
        playerInRange = false;

        bool isTouching = other.IsTouching(filter); //****BUG****
        if (isTouching)
        {
            return;
        }
        StopInteracting();
    }

}
