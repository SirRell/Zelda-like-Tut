using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Door door;
    public Sprite pressedSwitch;
    bool isPressed = false;
    Sprite unpressedSwitch;
    SpriteRenderer spriteRenderer;
    string uniqueID;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        unpressedSwitch = spriteRenderer.sprite;

        uniqueID = UnityEngine.SceneManagement.SceneManager.GetActiveScene() + name + transform.position;
        if (InfoManager.Instance.buttons.TryGetValue(uniqueID, out bool storedPressedBool))
        {
            isPressed = storedPressedBool;
            if (isPressed)
                UnlockDoor();
        }
        else
        {
            InfoManager.Instance.buttons.Add(uniqueID, isPressed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UnlockDoor();
        }
    }

    void UnlockDoor()
    {
        isPressed = true;
        InfoManager.Instance.buttons[uniqueID] = isPressed;

        spriteRenderer.sprite = pressedSwitch;
        door.SetDoorType(DoorType.None);
    }

    void LockDoor()
    {
        isPressed = false;
        InfoManager.Instance.buttons[uniqueID] = isPressed;

        spriteRenderer.sprite = unpressedSwitch;
        door.SetDoorType(DoorType.Switch);
    }
}
