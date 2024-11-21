using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("輪子物件設定")]

    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider backLeft;
    [SerializeField] WheelCollider backRight;

    [SerializeField] Transform frontLeftWheel;
    [SerializeField] Transform frontRightWheel;
    [SerializeField] Transform backLeftWheel;
    [SerializeField] Transform backRgihtWheel;



    [Header("汽車參數設定")]
    public float acceleration;
    public float breakForce;
    public float maxTurnAngle;
    public float maxSpeed;


    public Vector3 centerOfMass;

    Rigidbody rb;

    float currentAcceleration = 0;
    [SerializeField] float currentBreakForce = 0;
    float currentTurnAngle = 0;

    PlayerInputAction inputActions;
    CarAudioController carAudioController;
    private void Awake()
    {
        inputActions = new PlayerInputAction();
        carAudioController=GetComponent<CarAudioController>();
    }
    private void OnEnable()
    {
        inputActions.Enable();
        carAudioController.enabled = true;

    }
    private void OnDisable()
    {
        inputActions.Disable();
        carAudioController.enabled = false;

    }



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(rb.velocity.magnitude );

    }

    void FixedUpdate()
    {

        CarAcceleration();
        CarTurnAngel();


    }


    void CarAcceleration() //汽車加速功能
    {

        Debug.Log(inputActions.CarControl.Move.ReadValue<Vector3>().z);

        currentAcceleration = acceleration * inputActions.CarControl.Move.ReadValue<Vector3>().z;

        frontLeft.motorTorque = currentAcceleration;
        frontRight.motorTorque = currentAcceleration;




        LimitSpeed();
        StopCar();

    }

    void CarTurnAngel() //汽車轉向功能
    {
        currentTurnAngle = maxTurnAngle * inputActions.CarControl.Move.ReadValue<Vector3>().x;

        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;
        UpdateWheel(frontLeft, frontLeftWheel);
        UpdateWheel(frontRight, frontRightWheel);
        UpdateWheel(backLeft, backLeftWheel);
        UpdateWheel(backRight, backRgihtWheel);

    }
    void UpdateWheel(WheelCollider col, Transform trans) //更新輪子的轉向跟轉動
    {
        Vector3 posion;
        Quaternion rotation;
        col.GetWorldPose(out posion, out rotation);

        trans.position = posion;
        trans.rotation = rotation;
    }

    void StopCar()   //煞車功能
    {

        if (inputActions.CarControl.Break.ReadValue<float>() > 0)
        {
            currentBreakForce = breakForce;
        }
        else
        {
            if (inputActions.CarControl.Move.ReadValue<Vector3>().z == 0) //如果沒有按下前進按鈕(預設W、S)的話，汽車會有一個100的煞車值
            {
                currentBreakForce = 100;
            }
            else                                //否則如果有按下其中一個就沒有煞車
            {
                currentBreakForce = 0;
            }
        }
        frontLeft.brakeTorque = currentBreakForce;
        frontRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;


    }

    void LimitSpeed()       //限速
    {
        Vector3 currentSpeed = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (currentSpeed.magnitude > maxSpeed)
        {
            Vector3 limitSpeed = currentSpeed.normalized * maxSpeed;
            rb.velocity = new Vector3(limitSpeed.x, rb.velocity.y, limitSpeed.z);

        }
    }



}
