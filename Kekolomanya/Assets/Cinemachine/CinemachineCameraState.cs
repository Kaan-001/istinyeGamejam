using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineCameraState : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public static CinemachineCameraState Cinestate;
    void Start()
    {
        Cinestate = this;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
