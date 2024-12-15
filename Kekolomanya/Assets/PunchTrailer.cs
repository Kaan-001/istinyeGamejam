using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTrailer : MonoBehaviour
{
    public GameObject startPosition; // A pozisyonu
    public GameObject endPosition;

    void Start()
    {

        transform.position = startPosition.transform.position;
        transform.position = Vector3.Lerp(startPosition.transform.position, endPosition.transform.position, 2);
        Destroy(gameObject, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
