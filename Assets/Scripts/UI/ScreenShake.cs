using System.Collections;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    public float originalOrthoValue = 6f, endOrthoValue = 5.5f, duration, magnitude, ampGain = 0.4f;
    CinemachineVirtualCamera cam;
    CinemachineBasicMultiChannelPerlin camNoise;

    private void Start()
    {
        Player player = FindObjectOfType<Player>();
        player.DamageTaken += Shake;
        cam = GetComponent<CinemachineVirtualCamera>();
        camNoise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake()
    {
        if(isActiveAndEnabled)
            StartCoroutine(CamShake());
    }

    public IEnumerator CamShake()
    {
        float t = 0;
        camNoise.m_AmplitudeGain = ampGain;
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
        camNoise.m_AmplitudeGain = 0;
    }

    private void OnDisable()
    {
        cam.m_Lens.OrthographicSize = originalOrthoValue;
        camNoise.m_AmplitudeGain = 0;
    }
}
