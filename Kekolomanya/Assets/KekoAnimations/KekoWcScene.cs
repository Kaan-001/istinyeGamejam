using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
public class KekoWcScene : MonoBehaviour
{

    // Start is called before the first frame update
    public Transform KekoTransform, Hocatransform;
    public GameObject HocaObj;
    public Sprite Hoca, Keko, Player;
    public Volume GlobalVolume;

    void Start()
    {
        this.gameObject.transform.DOMoveX(KekoTransform.position.x, 4.5f).SetEase(Ease.Linear);

        StartCoroutine(SpeechKeko());

    }

    public IEnumerator SpeechKeko() 
    {
        // kulland���n stringdeki char ba��na 0.05 saniye tutup buna 1 saniye ekleyerek 

        this.gameObject.GetComponent<Animator>().Play("WalkKeko");
        yield return new WaitForSeconds(4.5f);
        this.gameObject.GetComponent<Animator>().Play("IdleKeko");
        string speechMan = "La TIRREK sen benim biricik Pelinsuyucuguma ne hakla. Ne c�rretle yan g�zle bakars�n heaggg!?";
        char[] stringsx = speechMan.ToCharArray();
        StartCoroutine(SpeechCode.SpeechMan.SpeechRoutine(speechMan,Keko));
      


        yield return new WaitForSeconds(stringsx.Length * 0.05f+2.5f);
        speechMan = "Muhammed abi vallaha ben birsey yapmadim.";
        stringsx = null;
        stringsx = speechMan.ToCharArray();
        StartCoroutine(SpeechCode.SpeechMan.SpeechRoutine(speechMan,Player));



        yield return new WaitForSeconds(stringsx.Length * 0.05f +2.5f);
        speechMan = "Kes lan Dingil!? Gel lan Buraya 50 kuru�unu yedi�im Murrooo!?!!";
        stringsx = null;
        stringsx = speechMan.ToCharArray();
        StartCoroutine(SpeechCode.SpeechMan.SpeechRoutine(speechMan,Keko));


        CinemachineCameraState.Cinestate.animator.Play("WcNoZoom");
        yield return new WaitForSeconds(stringsx.Length * 0.05f +3);
        HocaObj.transform.DOMoveX(Hocatransform.position.x, 0.25f);

        yield return new WaitForSeconds(1);

        //Hocanin girdi�i k�s�m sonra �fle kontrol edilir
        // while(PlayerHealth){}
        
        speechMan = "Oglum Hadi Problem ��karmay�n! Do�ru Derslere.";
        
        StartCoroutine(SpeechCode.SpeechMan.SpeechRoutine(speechMan, Hoca));
        yield return new WaitForSeconds(stringsx.Length * 0.05f + 4);
        if (GlobalVolume.profile.TryGet<Vignette>(out Vignette vignette))
        {
            // Burada Vignette'e eri�ilmi� olur. �imdi intensity'yi de�i�tirebilirsiniz.
            DOTween.To(() => vignette.intensity.value, x => vignette.intensity.Override(x), 1f, 1f); // �rne�in intensity'yi 0.5 olarak ayarla
        }

        HaritaArea.Leveller = new bool[4];
        HaritaArea.Leveller[0] = true;
        HaritaArea.Leveller[1] = true;
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("HaritaSahne");

    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
