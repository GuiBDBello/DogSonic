using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;
    public static AudioSource instance;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        instance = audioSource;
    }
}
