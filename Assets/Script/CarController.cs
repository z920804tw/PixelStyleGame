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
    float currentBreakForce = 0;
    float currentTurnAngle = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

        CarAcceleration();
        CarTurnAngel();

    }

    void CarAcceleration() //汽車加速功能
    {
        currentAcceleration = acceleration * Input.GetAxisRaw("Vertical");


        frontLeft.motorTorque = currentAcceleration;
        frontRight.motorTorque = currentAcceleration;

        frontLeft.brakeTorque = currentBreakForce;
        frontRight.brakeTorque = currentBreakForce;
        backLeft.brakeTorque = currentBreakForce;
        backRight.brakeTorque = currentBreakForce;

        LimitSpeed();
        StopCar();
    }

    void CarTurnAngel() //汽車轉向功能
    {
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
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
        if (Input.GetKey(KeyCode.Space))
        {
            currentBreakForce = breakForce;
        }
        else
        {
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                currentBreakForce = 100;
            }
            else
            {
                currentBreakForce = 0;
            }
        }


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
