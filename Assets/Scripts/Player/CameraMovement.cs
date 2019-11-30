using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform target;
    public float smoothing= 0.1f;
    public Vector2 maxPosition, minPosition;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        InfoManager iM = InfoManager.Instance;
        if (iM.NewPlayerPosition != Vector2.zero)
            transform.position = new Vector3(iM.NewPlayerPosition.x, iM.NewPlayerPosition.y, transform.position.y);
        if(iM.NewCameraBoundsMax != Vector2.zero || iM.NewCameraBoundsMin != Vector2.zero)
        {
            minPosition = iM.NewCameraBoundsMin;
            maxPosition = iM.NewCameraBoundsMax;
        }
    }

    void LateUpdate()
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, -5f);
        targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }
}
