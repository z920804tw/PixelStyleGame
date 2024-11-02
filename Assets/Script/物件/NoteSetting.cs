using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSetting : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject notePage;
    GameObject playerComponet;
    bool isOpen;
    void Start()
    {
        isOpen = false;
        playerComponet = GameObject.Find("PlayerComponets");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenNote()
    {
        if (!isOpen)
        {
            notePage.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            playerComponet.SetActive(false);
            isOpen = true;
        }
        else
        {
            notePage.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerComponet.SetActive(true);
            isOpen = false;
        }
    }
}
