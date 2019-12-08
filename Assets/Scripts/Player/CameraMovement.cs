using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraMovement : MonoBehaviour
{
    void Start()
    {
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<CinemachineVirtualCamera>().Follow = target;
        GetComponent<CinemachineConfiner>().m_BoundingShape2D = GetComponentInParent<PolygonCollider2D>();
    }
}
