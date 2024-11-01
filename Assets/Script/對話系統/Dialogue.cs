using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;


[Serializable]
public class DialogueInfo          //對話角色名稱、內容
{
    public string title;
    public string characterName;
    public string talkContent;

}
[Serializable]
public class CharacterInfo
{
    public TMP_Text characterName;
    public TMP_Text talkText;
}


[Serializable]
public class DialogueList                       //對話資訊的清單
{
    public List<DialogueInfo> dialogues;
}
