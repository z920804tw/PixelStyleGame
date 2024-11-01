using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSetting : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("車輛設定")]
    public GameObject player;
    public GameObject playerComponent;
    public Camera carCam;
    public Transform driverSeat;
    public Transform carTriggerZone;


    CarController carController;
    [Header("車內攝影機設定")]
    [SerializeField] float sensX;
    [SerializeField] float sensY;




    [Header("Debug")]
    Collider carCol;
    Collider playerCol;
    Rigidbody rb;
    public bool inTheCar = false;


    float xRotation;
    float yRotation;
    void Start()
    {
        carController = GetComponent<CarController>();
        carCol = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (!inTheCar)      //玩家上車
            {

                playerComponent.SetActive(false);

                playerCol = player.GetComponent<CapsuleCollider>(); //取得玩家碰撞體
                Physics.IgnoreCollision(carCol, playerCol, true);   //玩家跟汽車的碰撞無視

                player.transform.SetParent(driverSeat);       //設定玩家的位置、旋轉
                player.transform.position = driverSeat.position;
                player.transform.rotation = driverSeat.rotation;

                player.GetComponent<Rigidbody>().isKinematic = true; //關閉玩家的運動行為

                carCam.transform.localRotation = Quaternion.Euler(0, 0, 0);  //攝影機歸位

                carController.enabled = true;       //汽車操控打開

                inTheCar = true;

            }
            else
            {
                CarViewCam();
                if (Input.GetKeyDown(KeyCode.F) && Input.GetAxisRaw("Vertical")==0) //玩家下車
                {

                    playerComponent.SetActive(true);

                    player.transform.SetParent(null);
                    player.transform.position = new Vector3(carTriggerZone.position.x, carTriggerZone.position.y + 0.2f, carTriggerZone.position.z);
                    player.transform.rotation = Quaternion.identity;
                    player.GetComponent<Rigidbody>().isKinematic = false;

                    Physics.IgnoreCollision(carCol, playerCol, false);

                    player = null;
                    inTheCar = false;


                    carController.enabled = false;

                }
            }

        }

    }


    void CarViewCam()
    {

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -40, 40);
        yRotation = Mathf.Clamp(yRotation, -60, 60);

        carCam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
    }



}
