using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAudioController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("汽車音效設定")]
    //public CarAudio[] carAudio;

    public AudioSource carEngine;
    public AudioSource carSFX;

    public AudioClip carEngineClip;
    public AudioClip[] carSFXClips;

    [Header("汽車音效參數設定")]
    public float minSpeed;
    public float maxSpeed;
    public float minPitch;
    public float maxPitch;
    float currentSpeed;
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
        carEngine.Play();

    }
    private void OnDisable()
    {
        inputActions.Disable();
        carEngine.Pause();

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

        if (carEngine.clip != carEngineClip)
        {
            carEngine.clip = carEngineClip;
            carEngine.Play();

        }


        if (currentSpeed < minSpeed)
        {
            carEngine.pitch = minPitch;
        }
        else if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            carEngine.pitch = minPitch + pitchFromCar;
        }
        else if (currentSpeed >= maxSpeed)
        {
            carEngine.pitch = maxPitch;
        }

    }


    public void PlaySound(int i)
    {
        if (carSFXClips[i] != null)
        {
            carSFX.PlayOneShot(carSFXClips[i]);
        }

    }
}
