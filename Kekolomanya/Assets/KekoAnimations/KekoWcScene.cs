using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class KekoWcScene : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject ekranagelir;
    public Transform KekoTransform, Hocatransform;
    public GameObject HocaObj;
    public Sprite Hoca, Keko, Player;
    public Volume GlobalVolume;
    public static bool Fight=false;
    public GameObject KekoOBj;
    public AudioSource AudioSource;
    public AudioClip KekoAudio0, KekoAudio1, KekoAudio2, PlayerAudio,HocaAudio;
    void Start()
    {
        this.gameObject.transform.DOMoveX(KekoTransform.position.x, 4.5f).SetEase(Ease.Linear);

        StartCoroutine(SpeechKeko());

    }

    public IEnumerator SpeechKeko() 
    {
        // kulland���n stringdeki char ba��na 0.05 saniye tutup buna 1 saniye ekleyerek 
        Fight = false;
        this.gameObject.GetComponent<Animator>().Play("Walk");
        yield return new WaitForSeconds(4.5f);
        AudioSource.PlayOneShot(KekoAudio0);
        this.gameObject.GetComponent<Animator>().Play("IdleKeko");
        string speechMan = "La TIRREK sen benim biricik Pelinsuyucuguma Ne curretle yan gozle bakarsin heaggg!?";
        char[] stringsx = speechMan.ToCharArray();
        StartCoroutine(SpeechCode.SpeechMan.SpeechRoutine(speechMan,Keko));
      


        yield return new WaitForSeconds(stringsx.Length * 0.05f+2.5f);
        AudioSource.PlayOneShot(PlayerAudio);
        speechMan = "Jilet abi vallaha ben birsey yapmadim.";
        stringsx = null;
        stringsx = speechMan.ToCharArray();
        StartCoroutine(SpeechCode.SpeechMan.SpeechRoutine(speechMan,Player));

        yield return new WaitForSeconds(stringsx.Length * 0.05f +2.5f);
        speechMan = "Kes lan Dingil!? Gel lan Buraya simdi sen görürsün";
        AudioSource.PlayOneShot(KekoAudio1);
        stringsx = null;
        stringsx = speechMan.ToCharArray();
        StartCoroutine(SpeechCode.SpeechMan.SpeechRoutine(speechMan,Keko));


        CinemachineCameraState.Cinestate.animator.Play("WcNoZoom");
        yield return new WaitForSeconds(stringsx.Length * 0.05f +3);
        Fight = true;
        KekoOBj.GetComponent<EnemeyAI>().enabled = true;
        KekoOBj.GetComponent<Enemy>().enabled = true;
        KekoOBj.GetComponent<SpriteRenderer>().flipX = false;
        // şuan dövüş yapılması gereken şeyler eklenir


        // burada dövüş sahnesi başlar
        while (PlayerHealth.currentHealth>50)
         {
            yield return new WaitForSeconds(1);
        }
        HocaObj.transform.DOMoveX(Hocatransform.position.x, 0.25f);
        Fight = false;
        Destroy(KekoOBj.GetComponent<EnemeyAI>());
        Destroy(KekoOBj.GetComponent<Enemy>());
        this.gameObject.GetComponent<Animator>().Play("IdleKeko");
        yield return new WaitForSeconds(1);
       
        speechMan = "Oglum Hadi Problem cikarmayin! Dogru Derslere.";
        AudioSource.PlayOneShot(HocaAudio);
        StartCoroutine(SpeechCode.SpeechMan.SpeechRoutine(speechMan, Hoca));
        yield return new WaitForSeconds(stringsx.Length * 0.05f + 2);
        AudioSource.PlayOneShot(KekoAudio2);
        speechMan = "Bu is bitmedi cikista gorursun!!!";
        StartCoroutine(SpeechCode.SpeechMan.SpeechRoutine(speechMan, Keko));
        yield return new WaitForSeconds(stringsx.Length * 0.05f + 4);

        if (GlobalVolume.profile.TryGet<Vignette>(out Vignette vignette))
        {
            // Burada Vignette'e eri�ilmi� olur. �imdi intensity'yi de�i�tirebilirsiniz.
            DOTween.To(() => vignette.intensity.value, x => vignette.intensity.Override(x), 1f, 1f); // �rne�in intensity'yi 0.5 olarak ayarla
        }

        ekranagelir.transform.DOScale(1, 2.5f);
        HaritaArea.Leveller = new bool[4];
        HaritaArea.Leveller[0] = true;
        HaritaArea.Leveller[1] = true;
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("HaritaSahne");


    }
   
}
