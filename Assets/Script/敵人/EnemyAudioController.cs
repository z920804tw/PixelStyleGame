using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource enemySound;
    public AudioClip[] enemyClips;


    void Update()
    {

    }
    public void StopAllSound()
    {
        //enemySound.Stop();
        enemySound.clip = null;
    }

    public void PlaySound(int i)
    {
        if (enemyClips[i] != null)
        {
            enemySound.PlayOneShot(enemyClips[i]);
        }
    }

    public void AudioChange(int i)
    {
        if (enemySound.clip != enemyClips[i])
        {
            StopAllSound();
            enemySound.clip = enemyClips[i];
            enemySound.Play();
        }
    }

    // Update is called once per frame

}
