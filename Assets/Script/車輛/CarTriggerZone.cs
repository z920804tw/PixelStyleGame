using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarTriggerZone : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] CarSetting carSetting;

    [SerializeField] GameObject getOnCarText;

    bool isPlayerTrigger;


    void Start()
    {
        
        carSetting = transform.parent.GetComponent<CarSetting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerTrigger)
        {
            if (Input.GetKeyDown(KeyCode.F) && GameObject.Find("PlayerPick").GetComponent<PickController>().isHolding==false)
            {
                carSetting.player = GameObject.Find("Player");
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            getOnCarText.SetActive(true);
            
            isPlayerTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerTrigger = false;
            getOnCarText.SetActive(false);
        }
    }
}
