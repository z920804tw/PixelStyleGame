using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyAudio[] enemyAudio;
    void Awake()
    {
        foreach (EnemyAudio i in enemyAudio)
        {
            i.audioSource = gameObject.AddComponent<AudioSource>();
            i.audioSource.volume = i.volume;
            i.audioSource.pitch = i.pitch;

            i.audioSource.loop = i.isLoop;
            i.audioSource.spatialBlend = 1;


        }
    }


    void Update()
    {

    }
    public void StopAllSound()
    {
        foreach (EnemyAudio i in enemyAudio)
        {
            i.audioSource.Stop();
            i.audioSource.clip = null;
        }
    }

    // Update is called once per frame

}
