using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class SpeechCode : MonoBehaviour
{
    public GameObject SpeechObj;

    public Image speechmanImage;

    public string speechString;

    public TMP_Text speechText;
    public static SpeechCode SpeechMan;
    public void Start()
    {
        SpeechMan = this;
    }
    public IEnumerator SpeechRoutine(string currentSpeechString /*,Sprite willShow*/) 
    {
       // speechmanImage.sprite = willShow;

        speechText.text = "";
        
        SpeechObj.transform.DOLocalMoveY(-425, 1.25f);

        yield return new WaitForSeconds(1.5f);

        char[] speechChar = currentSpeechString.ToCharArray();

        for(int i = 0; i < speechChar.Length; i++) 
        {
            speechText.text += speechChar[i];

            yield return new WaitForSeconds(0.05f);
        }
       
        yield return new WaitForSeconds(1.5f);

        SpeechObj.transform.DOLocalMoveY(-900, 1.25f);

    }
}
