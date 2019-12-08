using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class ScreenShake : MonoBehaviour
{
    public float originalOrthoValue = 5, endOrthoValue, duration, magnitude;
    CinemachineVirtualCamera cam;
    private void Start()
    {
        Player player = FindObjectOfType<Player>();
        player.DamageTaken += Shake;
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    public void Shake()
    {
        if(isActiveAndEnabled)
            StartCoroutine(CamShake());
        transform.DOShakePosition(duration, magnitude);
    }

    public IEnumerator CamShake()
    {
        float t = 0;
        //Punch the camera inward
        while(cam.m_Lens.OrthographicSize != endOrthoValue)
        {
            cam.m_Lens.OrthographicSize = Mathf.Lerp(originalOrthoValue, endOrthoValue, t);
            t += Time.deltaTime * (1 / duration);
            yield return null;
        }

        t = 0;

        while (cam.m_Lens.OrthographicSize != originalOrthoValue)
        {
            cam.m_Lens.OrthographicSize = Mathf.Lerp(endOrthoValue, originalOrthoValue, t);
            t += Time.deltaTime * (1 / duration);
            yield return null;
        }
    }
}
