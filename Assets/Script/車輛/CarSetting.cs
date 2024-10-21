using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSetting : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("車輛設定")]
    public GameObject player;
    public Camera carCam;
    public Transform driverSeat;
    public Transform carTriggerZone;

    CarController carController;
    [SerializeField] float sensX;
    [SerializeField] float sensY;




    [Header("Debug")]
    Collider carCol;
    Collider playerCol;
    public bool inTheCar = false;
    bool hasSet;


    float xRotation;
    float yRotation;
    void Start()
    {
        carController = GetComponent<CarController>();
        carCol = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (!inTheCar)
            {
                if (!hasSet)//上車時的初始化
                {
                    player.GetComponent<PlayerController>().enabled = false;
                    player.GetComponent<PickController>().enabled = false;
                    playerCol = player.GetComponent<CapsuleCollider>();
                    Physics.IgnoreCollision(carCol, playerCol, true);

                    player.transform.SetParent(driverSeat);       //設定玩家的位置、旋轉
                    player.transform.position = driverSeat.position;
                    player.transform.rotation = driverSeat.rotation;
                    player.GetComponent<Rigidbody>().isKinematic = true; //關閉玩家的運動行為

                    //carCam.transform.localEulerAngles = new Vector3(0, 0, 0); //車內攝影機旋轉調正
                    carCam.transform.localRotation=Quaternion.Euler(0,0,0);

                    carController.enabled = true;

                    inTheCar = true;
                    hasSet = true;
                }
            }
            else
            {
                CarViewCam();
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (hasSet)
                    {
                        player.GetComponent<PlayerController>().enabled = true;
                        player.GetComponent<PickController>().enabled = true;

                        player.transform.SetParent(null);
                        player.transform.position = new Vector3(carTriggerZone.position.x, carTriggerZone.position.y + 0.2f, carTriggerZone.position.z);
                        player.transform.rotation = Quaternion.identity;
                        player.GetComponent<Rigidbody>().isKinematic = false;
                        Physics.IgnoreCollision(carCol, playerCol, false);
                        player = null;


                        inTheCar = false;
                        hasSet = false;

                        carController.enabled = false;
                        //this.enabled=false;



                    }
                }
            }
        }
    }


    void CarViewCam()
    {
        sensX = player.GetComponent<PlayerController>().sensX;
        sensY = player.GetComponent<PlayerController>().sensY;

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -40, 40);
        yRotation = Mathf.Clamp(yRotation, -60, 60);

        //carCam.transform.localEulerAngles = new Vector3(xRotation, yRotation, 0);
        carCam.transform.localRotation=Quaternion.Euler(xRotation,yRotation,0);
    }
}
