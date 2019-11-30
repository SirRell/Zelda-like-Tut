
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public string dialogue;

    protected override void Update()
    {
        base.Update();
        if(playerInRange && Input.GetButtonDown("Submit"))
        {
            dialogueBox.SetActive(!dialogueBox.activeSelf);
        }
    }

    override protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            base.OnTriggerEnter2D(other);
            dialogueText.text = dialogue;
        }
    }

    override protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            base.OnTriggerExit2D(other);
            dialogueBox.SetActive(false);
        }
    }
}
