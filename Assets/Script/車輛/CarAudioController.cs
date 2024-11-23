using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudioController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("汽車音效設定")]
    public CarAudio[] carAudio;
    [Header("汽車音效參數設定")]
    public float minSpeed;
    public float maxSpeed;
    float currentSpeed;
    float pitchFromCar;
    PlayerInputAction inputActions;
    Rigidbody rb;


    private void Awake()
    {
        inputActions = new PlayerInputAction();
        foreach (CarAudio i in carAudio)
        {
            i.audioSource=gameObject.AddComponent<AudioSource>();
            i.audioSource.volume=i.volume;
            i.audioSource.loop=i.isLoop;
        
        }


    }
    private void OnEnable()
    {
        inputActions.Enable();


    }
    private void OnDisable()
    {
        inputActions.Disable();
        foreach (CarAudio i in carAudio)
        {
            i.audioSource.clip = null;
            i.audioSource.Stop();
        }

    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        EngineSound();
    }


    void EngineSound()
    {
        currentSpeed = rb.velocity.magnitude;
        pitchFromCar = rb.velocity.magnitude / 50f;

        if (carAudio[0].audioSource.clip != carAudio[0].audioClip)
        {
            carAudio[0].audioSource.clip = carAudio[0].audioClip;
            carAudio[0].audioSource.Play();

        }


        if (currentSpeed < minSpeed)
        {
            carAudio[0].audioSource.pitch = carAudio[0].minPitch;
        }
        else if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            carAudio[0].audioSource.pitch = carAudio[0].minPitch + pitchFromCar;
        }
        else if (currentSpeed >= maxSpeed)
        {
            carAudio[0].audioSource.pitch = carAudio[0].maxPitch;
        }

    }


    public void PlaySound(string name)
    {
        foreach (CarAudio i in carAudio)
        {
            if (i.name == name)
            {
                i.audioSource.PlayOneShot(i.audioClip);
                break;
            }
        }

    }
}
