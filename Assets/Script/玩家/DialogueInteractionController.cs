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
    public Image crossHairs;
    public float maxRayDistance;






    [Header("Debug")]
    public bool isTalking;
    [SerializeField] GameObject cam;
    void Start()
    {
        PlayerController playerController = GameObject.Find("PlayerControl").GetComponent<PlayerController>();
        cam = playerController.PlayerCam;
    }

    // Update is called once per frame
    void Update()
    {
        TalkWithTarget();
    }

    void DetectTarget()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxRayDistance, canTalkLayer))
        {
            //crossHairs.color = Color.red;
        }
        else
        {
            //crossHairs.color = Color.white;
        }
        Debug.DrawRay(cam.transform.position, cam.transform.forward * maxRayDistance, Color.red);
    }

    void TalkWithTarget()
    {
        DetectTarget();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxRayDistance, canTalkLayer))
            {
                DialogueSystem dialogueSystem = hit.collider.gameObject.GetComponent<DialogueSystem>();
                if (dialogueSystem != null)
                {
                    dialogueSystem.TalkEvent.Invoke(); //觸發對話
                    StopPlayerControl();
                }

                isTalking = true;

            }
        }
    }

    void StopPlayerControl()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerComponet.SetActive(false);
    }


}
