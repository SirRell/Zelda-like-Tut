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
    protected GameObject player;
    ContextClue playerCC;
    ContactFilter2D filter;

    virtual protected void Start()
    {
        filter.useTriggers = filter.useLayerMask = true;
        filter.layerMask = 256; //256 is the "Interactable" layerMask
    }

    virtual protected void Update()
    {
        if (playerInRange && Input.GetButtonDown("Submit"))
        {
            Interacting();
        }
    }

    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.gameObject.GetComponentInParent<Player>().gameObject;
            playerCC = player.GetComponentInChildren<ContextClue>();
            MeInteractable();
        }
    }

    virtual protected void Interacting()
    {
        if (playerCC != null)
            playerCC.Interacting();
        //Interacting?.Invoke();
    }

    virtual protected void MeInteractable()
    {
       if(playerCC != null)
            playerCC.ShowContext(contextImage);
        //MeInteractable?.Invoke(contextImage);
    }

    virtual protected void StopInteracting()
    {
        if (playerCC != null)
            playerCC.StopInteracting();
        //NotInteracting?.Invoke();
    }

    virtual protected void OnTriggerExit2D(Collider2D other)
    {
        //BoxCollider2D other.GetComponent<BoxCollider2D>();
        if(other.CompareTag("Player"))
            playerInRange = false;

        bool isTouching = other.IsTouching(filter); 
        if (isTouching)
        {
            return;
        }
        StopInteracting();
    }

}
