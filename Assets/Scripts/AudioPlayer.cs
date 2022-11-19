using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip AudioClip;
    [Min(0)]
    public float Volume;

    private AudioSource _audio;
    

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _audio.clip = AudioClip;
        _audio.loop = true;
        _audio.Play(); 
    }

   
}
