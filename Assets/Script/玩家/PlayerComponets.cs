using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerComponets : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController playerController;
    public GameObject PlayerComponet;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool isInCar()
    {
        return playerController.isIncar;
    }


}
