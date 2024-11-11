using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("攝影機旋轉設定")]
    public GameObject PlayerCam;
    public float sensX;     //滑鼠靈敏度
    public float sensY;
    float xRotation;
    float yRotation;

    [Header("玩家移動設定")]
    public float moveSpeed;
    public float jumpForce;
    public float groundDrag;
    public float airDrag;
    [Header("玩家參數設定")]
    public float playerHeight;
    public LayerMask groundLayer;
    [Header("玩家跨越設定")]
    [SerializeField] GameObject maxCrossHeight;
    [SerializeField] GameObject minCrossHeight;
    [SerializeField] float stepMaxHeight;
    [SerializeField] float stepMinHeight;
    [SerializeField] float stepSmooth;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;

    PlayerInputAction inputActions;
    [Header("Debug")]
    public bool isIncar;
    [SerializeField] bool isGrounded;



    Rigidbody rb;
    private void Awake()
    {

        inputActions = new PlayerInputAction();
        maxCrossHeight.transform.localPosition = new Vector3(0, stepMaxHeight, 0);
        minCrossHeight.transform.localPosition = new Vector3(0, stepMinHeight, 0);
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    void Start()
    {


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = transform.root.GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        RotateCam();
        CheckOnGround();
        SpeedControl();

        if (inputActions.PlayerControl.Jump.WasPressedThisFrame() && isGrounded)
        {
            PlayerJump();
        }

    }

    private void FixedUpdate()
    {
        PlayerMovement();
        CheckCanAcross();
    }
    void CheckOnGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight);
        Debug.DrawRay(transform.position, Vector3.down * playerHeight, Color.red);
        if (isGrounded)
        {
            rb.drag = groundDrag;

        }
        else
        {
            rb.drag = 0;
        }
    }
    void CheckCanAcross()
    {
        Ray ray = new Ray(minCrossHeight.transform.position, moveDirection.normalized);
        RaycastHit hitLower;
        if (Physics.Raycast(ray, out hitLower, 0.2f, groundLayer))              //判斷有最低限制的設限有沒有打到東西
        {
            Debug.Log(hitLower.normal.y);
            Vector3 roundedNormal = new Vector3(Mathf.Floor(hitLower.normal.x) //檢查是不是走到斜坡
            , Mathf.CeilToInt(hitLower.normal.y),
            Mathf.Floor(hitLower.normal.z));
            Debug.Log(roundedNormal);

            if (roundedNormal != Vector3.up)
            {
                Ray ray1 = new Ray(maxCrossHeight.transform.position, moveDirection.normalized);    //會再判斷最高上限的設限有沒有打到物件
                RaycastHit hitUpper;                                                                //如果有打到就無法走上去,反之就可以
                if (!Physics.Raycast(ray1, out hitUpper, 0.2f))
                {
                    rb.position += new Vector3(0, stepSmooth, 0);
                }

            }
        }
    }
    void RotateCam()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -45, 50);

        PlayerCam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    void PlayerMovement()
    {
        horizontalInput = inputActions.PlayerControl.Move.ReadValue<Vector3>().x;
        verticalInput = inputActions.PlayerControl.Move.ReadValue<Vector3>().z;


        moveDirection = PlayerCam.transform.forward * verticalInput + PlayerCam.transform.right * horizontalInput;

        moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);//限制不會往天空飛



        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 2, ForceMode.Force);

        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airDrag, ForceMode.Force);
        }

    }

    void SpeedControl()
    {
        Vector3 currentSpeed = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //檢測目前的rb速度

        if (currentSpeed.magnitude > moveSpeed)
        {
            Vector3 limitSpeed = currentSpeed.normalized * moveSpeed;
            rb.velocity = new Vector3(limitSpeed.x, rb.velocity.y, limitSpeed.y);
        }
    }

    void PlayerJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
