using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyAudio
{
    public string name;

    public float volume;
    public float pitch;

    public bool isLoop;

    public AudioClip audioClip;
    public AudioSource audioSource;

}
