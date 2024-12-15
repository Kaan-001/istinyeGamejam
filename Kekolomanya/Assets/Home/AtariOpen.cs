using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtariOpen : MonoBehaviour
{
    public GameObject ThegameArea;
    bool OncePLay=false;
    public AtariManager AtariManager;
    public GameObject PLayer;
    void Update()
    {
        if(!OncePLay&& Input.GetKeyDown(KeyCode.E)) 
        {
            PLayer.SetActive(false);
            OncePLay = true;
            ThegameArea.SetActive(true);
            AtariManager.enabled = true;
        }
    }
}
