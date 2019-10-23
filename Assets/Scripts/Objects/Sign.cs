
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public string dialogue;
    public Sprite contextImage;
    bool playerInRange = false;
    public event System.Action<Sprite> Interactable;
    public event System.Action NotInteracting;
    public event System.Action Interacting;

    private void Update()
    {
        if(Input.GetButtonDown("Submit") && playerInRange)
        {
            Interacting?.Invoke();
            dialogueBox.SetActive(!dialogueBox.activeSelf);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Interactable?.Invoke(contextImage);
            dialogueText.text = dialogue;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            NotInteracting?.Invoke();
            playerInRange = false;
            dialogueBox.SetActive(false);
        }
    }
}
