using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ContextClue : MonoBehaviour
{
    //Interactable[] interactables;
    SpriteRenderer myRenderer;

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        
        //interactables = FindObjectsOfType<Interactable>();
        //foreach (Interactable interactable in interactables)
        //{
        //    interactable.MeInteractable += Interactable;
        //    interactable.NotInteracting += NotInteracting;
        //    interactable.Interacting += Interacting;
        //}
    }

    public void ShowContext(Sprite contextImage)
    {
        myRenderer.sprite = contextImage;
        myRenderer.enabled = true;
    }
    
    public void Interacting()
    {
        GetComponentInParent<Player>().ChangeState(PlayerState.Interact);
        myRenderer.enabled = !myRenderer.enabled;
    }

    public void StopInteracting()
    {
        GetComponentInParent<Player>().ChangeState(PlayerState.Walk);
        myRenderer.enabled = false;
    }
}
