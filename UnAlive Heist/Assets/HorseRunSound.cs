using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseRunSound : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}
