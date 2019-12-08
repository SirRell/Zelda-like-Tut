using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    AudioSource myAudio;
    public AudioClip swordSwing,
        playerDamaged,
        swordHit;


    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clip)
    {
        myAudio.PlayOneShot(clip);
    }
}
