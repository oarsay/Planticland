using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    // ENCAPSULATION
    [SerializeField] private AudioSource music, ambient;
    [SerializeField] private AudioSource effectSource;
    [SerializeField] private AudioClip[] effectClips;


    // POLYMORPHISM (method overloading)
    public void PlaySound()
    {
        effectSource.PlayOneShot(effectClips[Random.Range(0, effectClips.Length)]);
    }

    public void PlaySound(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }
}
