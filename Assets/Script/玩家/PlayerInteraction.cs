using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("參數設定")]
    public LayerMask canInteractiveLayer;
    public GameObject cam;
    public float maxRayDistance;
    public GameObject showHint;
    void Start()
    {
        cam = GameObject.Find("PlayerControl").GetComponent<PlayerController>().PlayerCam;
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
        if (Physics.Raycast(ray, out hit, maxRayDistance, canInteractiveLayer))
        {
            showHint.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerInteration(hit.collider.gameObject);
            }
        }
        else
        {
            //crossHairs.color = Color.white;
            showHint.SetActive(false);
        }
        Debug.DrawRay(cam.transform.position, cam.transform.forward * maxRayDistance, Color.red);
    }

    void PlayerInteration(GameObject hit)
    {
        hit.GetComponent<InteractableObject>().interactionEvent.Invoke();
    }



}
