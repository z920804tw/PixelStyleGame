using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSetting : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("車輛設定")]
    public Transform driverSeat;
    public Transform carTriggerZone;
    public GameObject player;
    GameObject playerComponent;
    PlayerController playerController;
    Camera carCam;

    CarController carController;
    [Header("車內攝影機設定")]
    [SerializeField] float sensX;
    [SerializeField] float sensY;

    [Header("車輛參數設定")]
    public float maxHp;
    public float carHp;
    public Image HealthBar;



    [Header("Debug")]
    Collider[] carCols;
    Collider playerCol;
    Rigidbody rb;
    public bool inTheCar = false;


    float xRotation;
    float yRotation;
    void Start()
    {
        playerComponent = GameObject.Find("PlayerComponets");
        playerController = GameObject.Find("PlayerControl").GetComponent<PlayerController>();
        carCam = GameObject.Find("Main Camera").GetComponent<Camera>();


        carController = GetComponent<CarController>();
        carCols = GetComponents<Collider>();

        rb = GetComponent<Rigidbody>();

        carHp = maxHp;
        HealthBar.fillAmount = carHp / maxHp;
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


                foreach (Collider i in carCols)
                {
                    Physics.IgnoreCollision(i, playerCol, true);   //玩家跟汽車的碰撞無視
                }

                player.transform.SetParent(driverSeat);       //設定玩家的位置、旋轉
                player.transform.position = driverSeat.position;
                player.transform.rotation = driverSeat.rotation;
                player.GetComponent<Rigidbody>().isKinematic = true; //關閉玩家的運動行為

                carCam.transform.localRotation = Quaternion.Euler(0, 0, 0);  //攝影機歸位
                carController.enabled = true;       //汽車操控打開
                




                inTheCar = true;
                playerController.isIncar = inTheCar;

            }
            else
            {
                CarViewCam();
                if (Input.GetKeyDown(KeyCode.F) && Input.GetAxisRaw("Vertical") == 0) //玩家下車
                {

                    playerComponent.SetActive(true);

                    player.transform.SetParent(null);
                    player.transform.position = new Vector3(carTriggerZone.position.x, carTriggerZone.position.y + 0.2f, carTriggerZone.position.z);
                    player.transform.rotation = Quaternion.identity;
                    player.GetComponent<Rigidbody>().isKinematic = false;

                    foreach (Collider i in carCols)
                    {
                        Physics.IgnoreCollision(i, playerCol, false);   //玩家跟汽車的碰撞無視
                    }

                    player = null;
                    inTheCar = false;
                    playerController.isIncar = inTheCar;
                    


                    carController.enabled = false;

                }
            }

        }

    }
    public void TakeDmg(int i)
    {
        carHp = carHp - i;
        HealthBar.fillAmount = carHp / maxHp;
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
