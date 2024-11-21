using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudioController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("汽車音效設定")]
    public AudioSource audioSource;
    public AudioClip carIdle;
    public AudioClip carAccelerateOn;
    public AudioClip carAccelerateOff;
    bool isTransitioning;

    [Header("汽車音效設定2")]
    public float minSpeed;
    public float maxSpeed;
    float currentSpeed;

    public float minPitch;
    public float maxPitch;
    float pitchFromCar;
    PlayerInputAction inputActions;
    Rigidbody rb;


    private void Awake()
    {
        inputActions = new PlayerInputAction();


    }
    private void OnEnable()
    {
        inputActions.Enable();


    }
    private void OnDisable()
    {
        inputActions.Disable();
        audioSource.clip = null;
        audioSource.Stop();

    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        //EngineSound();
        EngineSound2();
    }

    void EngineSound()
    {
        currentSpeed = rb.velocity.magnitude;
        pitchFromCar = rb.velocity.magnitude / 50f;



        if (inputActions.CarControl.Move.ReadValue<Vector3>().z == 0)
        {
            if (currentSpeed > minSpeed)
            {
                if (audioSource.clip != carAccelerateOff)
                {
                    audioSource.clip = carAccelerateOff;
                    audioSource.Play();
                }
                if (audioSource.pitch > minPitch)
                {
                    audioSource.pitch = minPitch + pitchFromCar;
                }
            }
            else
            {
                if (audioSource.clip != carIdle)
                {
                    audioSource.clip = carIdle;
                    audioSource.Play();
                }

                audioSource.pitch = maxPitch;
            }
        }
        else
        {
            if (audioSource.clip != carAccelerateOn)
            {
                audioSource.clip = carAccelerateOn;
                audioSource.Play();
            }

            if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
            {
                audioSource.pitch = minPitch + pitchFromCar;
            }
            else if (currentSpeed >= maxSpeed)
            {
                audioSource.pitch = maxPitch;
            }
        }
    }



    void EngineSound2()
    {
        if (audioSource.clip != carAccelerateOn)
        {
            audioSource.clip = carAccelerateOn;
            audioSource.Play();
        }
        currentSpeed = rb.velocity.magnitude;
        pitchFromCar = rb.velocity.magnitude / 50f;
        if (currentSpeed < minSpeed)
        {
            audioSource.pitch = minPitch;
        }
        else if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            audioSource.pitch = minPitch + pitchFromCar;
        }
        else if (currentSpeed >= maxSpeed)
        {
            audioSource.pitch = maxPitch;
        }

    }
}
