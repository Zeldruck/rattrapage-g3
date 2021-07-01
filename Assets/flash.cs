using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flash : MonoBehaviour
{
    public AudioClip sound;
    public AudioSource source;
    bool startSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf&&!startSound)
        {
            startSound = true;
            source.clip = sound;
            source.Play(); ;
            //StartCoroutine(Sound());
        }
        else if (!gameObject.activeSelf)
            startSound = false;
    }

}
