using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelinSu : MonoBehaviour
{
    public GameObject Interactive;
    public bool Speech = false, Once = true;
    public string[] NpcSpeech;
    public Sprite npcSprite, Player;
    public int SpeechNumber;
    public bool yoldas;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Speech = true;
            Interactive.SetActive(true);
        }
    }
    public void Update()
    {
        if (Speech && Input.GetKeyDown(KeyCode.E) && Once) 
        {
            Once = false;
            if (yoldas) 
            {
                CameraPosition.FighterCount += 1;
            }
            StartCoroutine(Chat());
        }
    }
    IEnumerator Chat()
    {
        Speech = false;
        Interactive.SetActive(false);
        //speechleri yazars�n
        if (NpcSpeech[SpeechNumber] != null) 
        {
            char[] stringsx = NpcSpeech[SpeechNumber].ToCharArray();

            StartCoroutine(SpeechCode.SpeechMan.SpeechRoutine(NpcSpeech[SpeechNumber],Player));

            SpeechNumber += 1;

            yield return new WaitForSeconds(stringsx.Length * 0.05f + 2f);

            stringsx = NpcSpeech[SpeechNumber].ToCharArray();

            StartCoroutine(SpeechCode.SpeechMan.SpeechRoutine(NpcSpeech[SpeechNumber], npcSprite));

            SpeechNumber += 1;

            yield return new WaitForSeconds(stringsx.Length * 0.05f + 2f);
        }
        SpeechNumber = 0;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Speech = false;
            Interactive.SetActive(false);
        }
    }
}
