using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelinSu : MonoBehaviour
{
    public GameObject Ýnteractive;
    public bool Speech = false, Once = true;
    public string[] NpcSpeech;
    public Sprite npcSprite, Player;
    public int SpeechNumber;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Speech = true;
            Ýnteractive.SetActive(true);
        }
    }
    public void Update()
    {
        if (Speech && Input.GetKeyDown(KeyCode.E) && Once) 
        {
            Once = false;

            StartCoroutine(Chat());
        }
    }
    IEnumerator Chat()
    {
        
        //speechleri yazarsýn
       
        char[] stringsx = NpcSpeech[SpeechNumber].ToCharArray();

        StartCoroutine(SpeechCode.SpeechMan.SpeechRoutine(NpcSpeech[SpeechNumber], npcSprite));

        SpeechNumber += 1;

        yield return new WaitForSeconds(stringsx.Length * 0.05f + 2.5f);
        Once = true;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Speech = true;
            Ýnteractive.SetActive(true);
        }
    }
}
