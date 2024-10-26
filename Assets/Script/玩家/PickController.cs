using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PickController : MonoBehaviour
{
    [Header("參數設定")]
    public Transform handPos;
    public LayerMask canPickLayer, placeLayer;
    public float maxRayDistance;
    public Image crossHairs;

    [Header("Debug")]
    [SerializeField] GameObject pickObject;
    public bool isHolding;
    Outline hitObjectOutline;
    Collider col;
    [SerializeField] GameObject cam;

    PlayerController playerController;


    void Start()
    {

        playerController = GameObject.Find("PlayerControl").GetComponent<PlayerController>();
        cam = playerController.PlayerCam;

        col = transform.root.GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        PickAndPlace();

    }
    void DetectObject()     //偵測可拿起物件和地板
    {
        Ray detcectRay = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (isHolding == false)
        {
            if (Physics.Raycast(detcectRay, out hit, maxRayDistance, canPickLayer))
            {
                //crossHairs.color = Color.green;
                hitObjectOutline = hit.collider.GetComponent<Outline>();
                hitObjectOutline.enabled = true;
            }
            else
            {
                if (hitObjectOutline != null)
                {
                    hitObjectOutline.enabled = false;
                    hitObjectOutline = null;
                }
                //crossHairs.color = Color.white;
            }
        }
        else
        {
            if (Physics.Raycast(detcectRay, out hit, maxRayDistance, placeLayer))
            {
                //crossHairs.color = Color.yellow;
            }
            else
            {
                //crossHairs.color = Color.white;
            }
        }
        Debug.DrawRay(cam.transform.position, cam.transform.forward * maxRayDistance, Color.red);
    }
    void PickAndPlace()
    {
        DetectObject();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hit;
            if (isHolding == false)
            {
                if (Physics.Raycast(ray, out hit, maxRayDistance, canPickLayer))
                {
                    pickObject = hit.collider.gameObject;
                    pickObject.transform.SetParent(handPos);
                    pickObject.transform.position = handPos.transform.position;
                    pickObject.transform.localEulerAngles = new Vector3(0, 0, 0);
                    pickObject.GetComponent<Rigidbody>().isKinematic = true;

                    hitObjectOutline.enabled = false;
                    Physics.IgnoreCollision(pickObject.GetComponent<Collider>(), col, true);

                    isHolding = true;
                }
            }
            else
            {
                if (Physics.Raycast(ray, out hit, maxRayDistance, placeLayer))
                {
                    pickObject.transform.SetParent(null);
                    pickObject.transform.position = new Vector3(hit.point.x, hit.point.y + 0.2f, hit.point.z);
                    pickObject.transform.rotation = Quaternion.identity;
                    pickObject.GetComponent<Rigidbody>().isKinematic = false;
                    Physics.IgnoreCollision(pickObject.GetComponent<Collider>(), col, false);

                    pickObject = null;
                    isHolding = false;
                }
            }
        }
    }
}
