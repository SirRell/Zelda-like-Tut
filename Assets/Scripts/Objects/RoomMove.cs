using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraBoundsChange;
    public Vector2 playerPosShift;
    CameraMovement cam;
    public bool needText;
    public string roomName;
    public GameObject text;
    TextMeshProUGUI placeText;

    private void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
        placeText = text.GetComponent<TextMeshProUGUI>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cam.minPosition += cameraBoundsChange;
            cam.maxPosition += cameraBoundsChange;

            other.transform.position += (Vector3)playerPosShift;

            if (needText)
            {
                StartCoroutine(PlaceNameCo());
            }
        }
        else
        {
            print("That's not the player, it's " + other.name);
        }
    }

    IEnumerator PlaceNameCo()
    {
        text.SetActive(true);
        placeText.text = roomName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }

}
