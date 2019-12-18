using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public enum DoorType
{
    None,
    Key,
    UncommonKey,
    BossKey,
    KillEnemies,
    Switch
}

public class Door : Interactable
{
    public DoorType OpenBy;
    public Sprite openDoorSprite;
    Sprite closedDoorSprite;
    SpriteRenderer spriteRenderer;

    float shakeDuration = .2f, shakeStrength = .2f;
    int vibration = 50;
    int enemiesLeft = 0;

    RoomMove rm;

    string uniqueID;


    protected override void Start()
    {
        base.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
        closedDoorSprite = spriteRenderer.sprite;
         rm = GetComponent<RoomMove>();

        uniqueID = UnityEngine.SceneManagement.SceneManager.GetActiveScene() + name + transform.position;
        if (InfoManager.Instance.doors.TryGetValue(uniqueID, out DoorType temp))
        {
            OpenBy = temp;
            if (OpenBy == DoorType.None)
            {
                SetDoorType(DoorType.None);
            }
        }
        else
        {
            InfoManager.Instance.doors.Add(uniqueID, OpenBy);
        }

    }

    protected override void Interacting()
    {
        Inventory pI = player.GetComponent<Inventory>();

        switch (OpenBy)
        {
            case DoorType.None:
                StartCoroutine(OpenDoor());
                break;
            case DoorType.Key:
                if(pI.commonKeys > 0)
                {
                    pI.RemoveItem(ItemType.CommonKey);
                    StartCoroutine(OpenDoor());
                }
                else
                {
                    ShakeDoor();
                }
                break;
            case DoorType.UncommonKey:
                if(pI.uncommonKeys > 0)
                {
                    pI.RemoveItem(ItemType.UncommonKey);
                    StartCoroutine(OpenDoor());
                }
                else
                {
                    ShakeDoor();
                }
                break;
            case DoorType.BossKey:
                if (pI.bossKeys > 0)
                {
                    pI.RemoveItem(ItemType.BossKey);
                    StartCoroutine(OpenDoor());
                }
                else
                {
                    ShakeDoor();
                }
                break;
            case DoorType.KillEnemies:
                if(enemiesLeft <= 0)
                {
                    StartCoroutine(OpenDoor());
                }
                else
                {
                    ShakeDoor();
                }
                break;
            case DoorType.Switch:
                ShakeDoor();
                break;
            default:
                break;
        }
    }

    void ShakeDoor()
    {
        transform.DOShakePosition(shakeDuration, shakeStrength, vibration, 90);
    }

    IEnumerator OpenDoor()
    {
        SetDoorType(DoorType.None);
        InfoManager.Instance.doors[uniqueID] = DoorType.None;
        player.GetComponent<PlayerMovement>().enabled = false;
        spriteRenderer.sprite = openDoorSprite;
        yield return new WaitForSeconds(.5f);
        rm.MovePlayer(player.GetComponent<Transform>());
        spriteRenderer.sprite = closedDoorSprite;
        yield return new WaitForSeconds(.2f);
        Destroy(rm);
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    public void SetDoorType(DoorType newOpenBy)
    {
        if (OpenBy != newOpenBy)
            OpenBy = newOpenBy;
    }
}
