using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coridor : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite Keko, Hoca, PelinSu, HamileHoca;
    void Start()
    {
        
    }
    IEnumerator Chat() 
    {
        string speechMan = "La TIRREK sen benim biricik Pelinsuyucuguma ne hakla. Ne cürretle yan gözle bakarsýn heaggg!?";
        char[] stringsx = speechMan.ToCharArray();
        StartCoroutine(SpeechCode.SpeechMan.SpeechRoutine(speechMan,Keko));



        yield return new WaitForSeconds(stringsx.Length * 0.05f + 2.5f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
