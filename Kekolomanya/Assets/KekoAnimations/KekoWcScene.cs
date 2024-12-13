using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class KekoWcScene : MonoBehaviour
{

    // Start is called before the first frame update
    public Transform KekoTransform;
    void Start()
    {
        this.gameObject.transform.DOMoveX(KekoTransform.position.x, 4.5f).SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
