using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CheckPonit : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent unityEvent;
    public GameObject hint;
    public GameObject progressBar;
    public bool isFinish;
    bool isStay;

    public float progressValue;


    void Start()
    {
        progressValue = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (isStay)
        {
            if (Input.GetKey(KeyCode.E))
            {
                hint.SetActive(false);
                progressBar.SetActive(true);

                progressValue += Time.deltaTime;
                progressBar.GetComponent<Image>().fillAmount = progressValue / 5;

                if (progressValue >= 5)
                {
                    Debug.Log("完成!");
                    isFinish = true;
                    progressBar.SetActive(false);
                    unityEvent.Invoke();

                    this.gameObject.SetActive(false);
                    
                }
            }
            else
            {
                if (progressValue <= 0)
                {
                    progressValue = 0;
                    hint.SetActive(true);
                    progressBar.SetActive(false);
                    return;
                }
                progressValue -= Time.deltaTime;
                progressBar.GetComponent<Image>().fillAmount = progressValue / 5;

            }
        }
        else
        {
            progressValue = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            isStay = true;
            hint.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            isStay = false;
            hint.SetActive(false);
            progressBar.SetActive(false);
        }
    }
}
