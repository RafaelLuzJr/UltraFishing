using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header ("----Audio Source----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----Audio Clips----")]
    public AudioClip normalShoot;
    public AudioClip swordShoot;
    public AudioClip pickupFish;
    public AudioClip getHit;
    public AudioClip powerUpFilled;
    public AudioClip death;
    public AudioClip backgroundMsc;

    private void Start()
    {
        musicSource.clip = backgroundMsc;
        musicSource.Play();
    }

    public void PlaySndEffects(AudioClip audio)
    {
        SFXSource.PlayOneShot(audio);
    }
}
