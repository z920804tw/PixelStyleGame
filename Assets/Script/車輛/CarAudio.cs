using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CarAudio
{
    public string name;

    public float volume;
    public float minPitch;
    public float maxPitch;
    public bool isLoop;
    public AudioClip audioClip;
    public AudioSource audioSource;

}
