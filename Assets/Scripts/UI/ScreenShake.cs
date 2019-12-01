using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenShake : MonoBehaviour
{
    public float endValue, duration, magnitude;
    Camera cam;
    private void Start()
    {
        Player player = FindObjectOfType<Player>();
        player.DamageTaken += Shake;
        cam = GetComponent<Camera>();
    }

    public void Shake()
    {
        StartCoroutine(CamShake());
    }

    public IEnumerator CamShake()
    {
        transform.DOShakePosition(duration, magnitude);

        cam.DOOrthoSize(endValue, duration / 2);
        yield return new WaitForSeconds(duration / 2);
        cam.DOOrthoSize(6, duration / 2);
    }
}
