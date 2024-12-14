using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class HaritaArea : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool[] Leveller;
    public GameObject[] obj;
    public Volume GlobalVolume;
    public int levelOfScene;
    void Start()
    {
        for (int i = 0; i < Leveller.Length; i++)
        {
            if (Leveller[i] == true)
            {
                obj[i].transform.GetChild(0).transform.DOScale(1,1);
            }
            else
            {
              
                levelOfScene = i;
                StartCoroutine(LevelChanger());
                break;
            }
        }
        
    }

    IEnumerator LevelChanger() 
    {
        obj[levelOfScene].transform.GetChild(1).transform.DOScale(1, 1);
        yield return new WaitForSeconds(4f);
        if (GlobalVolume.profile.TryGet<Vignette>(out Vignette vignette))
        {
            // Burada Vignette'e eriþilmiþ olur. Þimdi intensity'yi deðiþtirebilirsiniz.
            DOTween.To(() => vignette.intensity.value, x => vignette.intensity.Override(x), 1, 1f); // Örneðin intensity'yi 0.5 olarak ayarla
        }
        yield return new WaitForSeconds(1.5f);
        switch(levelOfScene)
        {
            case 0: SceneManager.LoadScene(0); break;
            case 1: SceneManager.LoadScene(1); break;
            case 2: SceneManager.LoadScene(2); break;
            case 3: SceneManager.LoadScene(3); break;
        }
        
    }
    

    
}
