using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TextController : MonoBehaviour
{
    // Start is called before the first frame update


    public TextAsset text;

    public TMP_Text Talktext;
    public GameObject button;


    public float TextSpeed = 0.05f;

    string word;
    bool isTyping;

    private void OnEnable()
    {

        button.SetActive(false);

        StartCoroutine(addText());
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (isTyping)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Talktext.text = word;
            }
        }
    }

    IEnumerator addText()
    {

        word = text.ToString();
        Talktext.text = string.Empty;
        foreach (char c in word.ToCharArray())
        {
            if (Talktext.text.Length >= word.Length)
            {
                isTyping = false;
                button.SetActive(true);
                yield break;
            }
            else
            {
                Talktext.text += c;
                yield return new WaitForSeconds(TextSpeed);
                isTyping = true;
            }
        }
        Debug.Log("結束");
        isTyping=false;
        button.SetActive(true);
    }
}
