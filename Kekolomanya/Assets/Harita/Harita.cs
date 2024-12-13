using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class Harita : MonoBehaviour
{

    public Volume GlobalVolume;
    void Start()
    {
        if (GlobalVolume.profile.TryGet<Vignette>(out Vignette vignette))
        {
            // Burada Vignette'e eriþilmiþ olur. Þimdi intensity'yi deðiþtirebilirsiniz.
            DOTween.To(() => vignette.intensity.value, x => vignette.intensity.Override(x), 0, 1f); // Örneðin intensity'yi 0.5 olarak ayarla
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
