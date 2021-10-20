using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    // Start is called before the first frame update
    //AudioSource audioSource;
    public AudioSource audioSource;
    void Start()
    {
       // audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            audioSource.mute = !audioSource.mute;
    }
}