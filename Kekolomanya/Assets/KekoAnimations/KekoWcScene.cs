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
        // kullandýðýn stringdeki char baþýna 0.05 saniye tutup buna 1 saniye ekleyerek 


        yield return new WaitForSeconds(4.5f);

        string speechMan = "La TIRREK sen benim biricik Pelinsuyucuguma ne hakla. Ne cürretle yan gözle bakarsýn heaggg!?";
        char[] stringsx = speechMan.ToCharArray();
        StartCoroutine(SpeechCode.SpeechMan.SpeechRoutine(speechMan,Keko));
      


        yield return new WaitForSeconds(stringsx.Length * 0.05f+2.5f);
        speechMan = "Muhammed abi vallaha ben birsey yapmadim.";
        stringsx = null;
        stringsx = speechMan.ToCharArray();
        StartCoroutine(SpeechCode.SpeechMan.SpeechRoutine(speechMan,Player));



        yield return new WaitForSeconds(stringsx.Length * 0.05f +2.5f);
        speechMan = "Kes lan Dingil!? Gel lan Buraya 50 kuruþunu yediðim Murrooo!?!!";
        stringsx = null;
        stringsx = speechMan.ToCharArray();
        StartCoroutine(SpeechCode.SpeechMan.SpeechRoutine(speechMan,Keko));


        CinemachineCameraState.Cinestate.animator.Play("WcNoZoom");
        yield return new WaitForSeconds(stringsx.Length * 0.05f +3);
        HocaObj.transform.DOMoveX(Hocatransform.position.x, 0.25f);

        yield return new WaitForSeconds(1);

        //Hocanin girdiði kýsým sonra Ýfle kontrol edilir
        // while(PlayerHealth){}
        
        speechMan = "Oglum Hadi Problem çýkarmayýn! Doðru Derslere.";
        
        StartCoroutine(SpeechCode.SpeechMan.SpeechRoutine(speechMan, Hoca));
        yield return new WaitForSeconds(stringsx.Length * 0.05f + 4);
        if (GlobalVolume.profile.TryGet<Vignette>(out Vignette vignette))
        {
            // Burada Vignette'e eriþilmiþ olur. Þimdi intensity'yi deðiþtirebilirsiniz.
            DOTween.To(() => vignette.intensity.value, x => vignette.intensity.Override(x), 1f, 1f); // Örneðin intensity'yi 0.5 olarak ayarla
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("HaritaSahne");

    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
