using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ContextClue : MonoBehaviour
{
    Sign[] signs;
    SpriteRenderer myRenderer;

    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        signs = FindObjectsOfType<Sign>();
        foreach (Sign sign in signs)
        {
            sign.Interactable += Interactable;
            sign.NotInteracting += NotInteracting;
            sign.Interacting += Interacting;
        }
    }

    void Interactable(Sprite contextImage)
    {
        GetComponentInParent<Player>().ChangeState(Player.PlayerState.Interact);
        myRenderer.sprite = contextImage;
        myRenderer.enabled = true;
    }

    void Interacting()
    {
        myRenderer.enabled = !myRenderer.enabled;
    }

    void NotInteracting()
    {
        GetComponentInParent<Player>().ChangeState(Player.PlayerState.Walk);

        myRenderer.enabled = false;
    }
}
