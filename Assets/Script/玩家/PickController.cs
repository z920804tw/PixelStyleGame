using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PickController : MonoBehaviour
{
    [Header("參數設定")]
    public Transform handPos;

    public float maxRayDistance;
    public GameObject ShowHint;
    public LayerMask canPickLayer, placeLayer;

    PlayerInputAction inputActions;



    [Header("Debug")]
    [SerializeField] GameObject pickObject;
    [SerializeField] GameObject cam;
    public bool isHolding;
    Outline hitObjectOutline;
    Collider col;

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
        cam = GameObject.Find("PlayerControl").GetComponent<PlayerController>().PlayerCam;
        col = transform.root.GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        DetectObject();
    }
    void DetectObject()     //偵測可拿起物件和地板
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (!isHolding)
        {
            if (Physics.Raycast(ray, out hit, maxRayDistance, canPickLayer))
            {
                hitObjectOutline = hit.collider.GetComponent<Outline>();
                hitObjectOutline.enabled = true;
                ShowHint.SetActive(true);

                if (inputActions.PlayerControl.PickPlace.WasPressedThisFrame())
                {
                    PickObject(hit.collider.gameObject);
                }
            }
            else
            {
                if (hitObjectOutline != null)
                {
                    hitObjectOutline.enabled = false;
                    hitObjectOutline = null;
                }
                ShowHint.SetActive(false);
            }
        }
        else
        {
            if (Physics.Raycast(ray, out hit, maxRayDistance, placeLayer))
            {
                if (inputActions.PlayerControl.PickPlace.WasPressedThisFrame())
                {
                    DropObject(hit);
                }
            }

        }

        Debug.DrawRay(cam.transform.position, cam.transform.forward * maxRayDistance, Color.red);
    }

    void PickObject(GameObject hit)
    {
        pickObject = hit;
        pickObject.transform.SetParent(handPos);
        pickObject.transform.position = handPos.transform.position;
        pickObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        pickObject.GetComponent<Rigidbody>().isKinematic = true;

        hitObjectOutline.enabled = false;
        //Physics.IgnoreCollision(pickObject.GetComponent<Collider>(), col, true);
        pickObject.GetComponent<Collider>().enabled = false;

        ShowHint.SetActive(false);
        isHolding = true;

    }

    void DropObject(RaycastHit hit)
    {
        pickObject.transform.SetParent(null);
        pickObject.transform.position = new Vector3(hit.point.x, hit.point.y + 0.2f, hit.point.z);
        pickObject.transform.rotation = Quaternion.identity;
        pickObject.GetComponent<Rigidbody>().isKinematic = false;
        //Physics.IgnoreCollision(pickObject.GetComponent<Collider>(), col, false);
        pickObject.GetComponent<Collider>().enabled = true;

        pickObject = null;
        isHolding = false;

    }
}
