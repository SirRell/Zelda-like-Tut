using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomMove : MonoBehaviour
{
    public Vector2 playerPosShift;

    public bool needText;
    public string roomName;
    TextMeshProUGUI placeText;

    private void Start()
    {
        placeText = GameObject.Find("RoomTitle").GetComponent<TextMeshProUGUI>();
    }

    public void MovePlayer(Transform player)
    {
        if (player.CompareTag("Player"))
        {
            player.position += (Vector3)playerPosShift;

            if (needText)
            {
                StartCoroutine(PlaceNameCo());
            }
        }
    }

    IEnumerator PlaceNameCo()
    {
        placeText.text = roomName;
        placeText.enabled = true;
        yield return new WaitForSeconds(4f);
        placeText.enabled = false;
    }

}
