using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision other)
    {

        rb.isKinematic = false;
        Debug.Log("enter");


    }
    private void OnCollisionExit(Collision other)
    {

        rb.isKinematic = true;
        Debug.Log("exit");
        // isCol=false;


    }
}
