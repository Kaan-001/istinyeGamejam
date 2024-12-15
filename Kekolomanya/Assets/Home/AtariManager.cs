using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AtariManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Image Gemi;
    public Image[] enemy;
    public float moveSpeed = 500f;   // Hareket h�z�
    public float minX = -435f;       // Sol s�n�r
    public float maxX = 435f;        // Sa� s�n�r
    public RectTransform spawnPoint;
    public RectTransform rectTransform; // UI eleman�n�n RectTransform bile�eni
    public GameObject Mermi;
    
    
    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Instantiate the bullet
            GameObject bullet = Instantiate(Mermi, spawnPoint.position, Quaternion.identity);

            // Set the parent to be the Canvas (the parent of the spawnPoint)
            bullet.transform.SetParent(spawnPoint.parent, false);

            // Get the RectTransform of the bullet to adjust the position
            RectTransform bulletRectTransform = bullet.GetComponent<RectTransform>();

            // Position the bullet in front of the ship
            bulletRectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + 50); // Adjust +50 for vertical distance
        }

    }
    void Movement() 
    {
        // Kullan�c�dan yatay giri� al (A, D tu�lar� veya ok tu�lar�)
        float moveInput = Input.GetAxis("Horizontal");

        // Mevcut anchoredPosition'u al
        Vector2 position = rectTransform.anchoredPosition;

        // X ekseninde hareketi uygula
        position.x += moveInput * moveSpeed * Time.deltaTime;

        // X pozisyonunu s�n�rland�r
        position.x = Mathf.Clamp(position.x, minX, maxX);

        // Yeni pozisyonu geri uygula
        rectTransform.anchoredPosition = position;
    }
}
