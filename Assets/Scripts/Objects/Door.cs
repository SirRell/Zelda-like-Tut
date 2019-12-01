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
    public Vector2 cameraBoundChange;
    public Vector2 playerPosShift;
    float shakeDuration = .2f, shakeStrength = .2f;
    int vibration = 50;
    int enemiesLeft = 0;

    protected override void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        closedDoorSprite = spriteRenderer.sprite;

        base.Start();
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
                    pI.commonKeys--;
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
                    pI.uncommonKeys--;
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
                    pI.bossKeys--;
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
        player.GetComponent<PlayerMovement>().enabled = false;
        spriteRenderer.sprite = openDoorSprite;
        RoomMove rm = gameObject.AddComponent<RoomMove>();
        rm.cameraBoundsChange = cameraBoundChange;
        rm.playerPosShift = playerPosShift;
        yield return new WaitForSeconds(.5f);
        rm.OnTriggerEnter2D(player.GetComponent<Collider2D>());
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
