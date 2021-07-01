using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private AudioSource asc;

    private void Awake()
    {
        asc = GameObject.Find("Audio SourceThunder").GetComponent<AudioSource>();
    }

    private void PlayThunderSound()
    {
        if (asc != null)
            asc.Play();
    }
}
