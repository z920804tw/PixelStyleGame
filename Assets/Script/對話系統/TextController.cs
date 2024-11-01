using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TextController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("參數設定")]
    public CharacterInfo characterInfo;  //角色資訊，裡面包含名稱、對話內容
    DialogueSystem dialogueSystem;       //要取得


    public GameObject button;                   //按鈕顯示
    public float TextSpeed = 0.05f;             //文字顯示速度

    [Tooltip("設定這是第幾個對話")]
    public int currentIndex = 0;



    string word;
    bool isTyping;

    private void Awake()
    {
        dialogueSystem = transform.root.GetComponent<DialogueSystem>();
        word = dialogueSystem.dialogueList.dialogues[currentIndex].talkContent; //設定word內容等於json檔案裡面的第幾個對話
        characterInfo.characterName.text = dialogueSystem.dialogueList.dialogues[currentIndex].characterName; //取的json的角色名稱
    }
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
                characterInfo.talkText.text = word;
            }
        }
    }

    IEnumerator addText()               //對話顯示
    {
        characterInfo.talkText.text = string.Empty;
        foreach (char c in word.ToCharArray())
        {
            if (characterInfo.talkText.text.Length < word.Length)
            {
                characterInfo.talkText.text += c;
                yield return new WaitForSeconds(TextSpeed);
                isTyping = true;
            }
            else{}
        }
        Debug.Log("結束");
        isTyping = false;
        button.SetActive(true);
    }
}
