using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueInteractionController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("參數設定")]
    public LayerMask canTalkLayer;
    public GameObject playerComponet;
    public float maxRayDistance;

    public GameObject ShowHint;

    PlayerInputAction inputActions;

    [Header("Debug")]
    public bool isTalking;
    [SerializeField] GameObject cam;


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
    }

    void Start()
    {
        PlayerController playerController = GameObject.Find("PlayerControl").GetComponent<PlayerController>();
        cam = playerController.PlayerCam;
    }

    // Update is called once per frame
    void Update()
    {
        DetectTarget();
    }

    void DetectTarget()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxRayDistance, canTalkLayer))
        {
            ShowHint.SetActive(true);
            if (inputActions.PlayerControl.Talk.WasPressedThisFrame())
            {
                TalkWithTarget(hit.collider.gameObject);
            }
        }
        else
        {
            ShowHint.SetActive(false);

        }
        Debug.DrawRay(cam.transform.position, cam.transform.forward * maxRayDistance, Color.red);
    }

    void TalkWithTarget(GameObject hit)
    {
        DialogueSystem dialogueSystem = hit.GetComponent<DialogueSystem>();
        if (dialogueSystem != null)
        {
            dialogueSystem.TalkEvent.Invoke(); //觸發對話
        }
        StopPlayerControl();
        ShowHint.SetActive(false);
        isTalking = true;
    }

    void StopPlayerControl()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerComponet.SetActive(false);
    }


}
