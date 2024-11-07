using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class OpenGoodsDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationAngle;
    bool isOpen;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenGoodsDoors()
    {
        if (!isOpen)
        {
            this.transform.localRotation = Quaternion.Euler(0, rotationAngle, 0);
        }
        else
        {
            this.transform.localRotation = Quaternion.Euler(0,0,0);
        }
        isOpen=!isOpen;
    }
}