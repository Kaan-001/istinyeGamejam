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

   

    public TMP_Text speechText;
    public static SpeechCode SpeechMan;
    public void Start()
    {
        SpeechMan = this;
    }
    public IEnumerator SpeechRoutine(string currentSpeechString, Sprite willShow) 
    {
        speechmanImage.sprite = willShow;
        switch (willShow.name) 
        {
            case "Keko": speechmanImage.transform.DOLocalMoveX(745, 0);
                break;
            case "Hoca":
                speechmanImage.transform.DOLocalMoveX(745, 0);
                break;

            case "Player":
                speechmanImage.transform.DOLocalMoveX(-665, 0);

                break;
        }

        speechText.text = "";
        
        SpeechObj.transform.DOLocalMoveY(0, 0.5f);

        yield return new WaitForSeconds(0.5f);

        char[] speechChar = currentSpeechString.ToCharArray();

        for(int i = 0; i < speechChar.Length; i++) 
        {
            speechText.text += speechChar[i];

            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        SpeechObj.transform.DOLocalMoveY(-1200, 0.5f);
      
        

    }
}
