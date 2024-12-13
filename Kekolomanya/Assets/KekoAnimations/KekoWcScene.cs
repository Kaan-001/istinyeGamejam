using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class KekoWcScene : MonoBehaviour
{

    // Start is called before the first frame update
    public Transform KekoTransform;

    public string KekoString;

    void Start()
    {
        this.gameObject.transform.DOMoveX(KekoTransform.position.x, 4.5f).SetEase(Ease.Linear);
        Invoke("SpeechKeko", 4.5f);
    }
    public void SpeechKeko() 
    {
        StartCoroutine(SpeechCode.SpeechMan.SpeechRoutine(KekoString));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
