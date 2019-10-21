using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing= 0.6f;
    public Vector2 maxPosition, minPosition;


    void Start()
    {
        InfoManager iM = InfoManager.Instance;
        if (iM.NewPlayerPosition != Vector2.zero)
            transform.position = new Vector3(iM.NewPlayerPosition.x, iM.NewPlayerPosition.y, transform.position.y);
    }

    void LateUpdate()
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, -5f);
        targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }
}
