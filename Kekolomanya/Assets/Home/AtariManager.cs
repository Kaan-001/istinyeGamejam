using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class AtariManager : MonoBehaviour
{
    public static int newEra;
    // Start is called before the first frame update
    public Image Gemi;
    public Image[] enemy;
    public float moveSpeed = 500f;   // Hareket hýzý
    public float minX = -435f;       // Sol sýnýr
    public float maxX = 435f;        // Sað sýnýr
    public RectTransform spawnPoint;
    public RectTransform rectTransform; // UI elemanýnýn RectTransform bileþeni
    public GameObject Mermi;
    public Image Stick, Buttonx;
    public Sprite Sag, sol, Orta,push,Nopush;
    IEnumerator coroutin() 
    {
        Buttonx.sprite = push;
        yield return new WaitForSeconds(2f);
        Buttonx.sprite = Nopush;
    }
    
    void Update()
    {
        if (newEra == 6) 
        {
            HaritaArea.Leveller = new bool[4];
            HaritaArea.Leveller[0] = true;
            SceneManager.LoadScene("HaritaSahne");

        }
        Movement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(coroutin());
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
        // Kullanýcýdan yatay giriþ al (A, D tuþlarý veya ok tuþlarý)
        float moveInput = Input.GetAxis("Horizontal");

        // Mevcut anchoredPosition'u al
        Vector2 position = rectTransform.anchoredPosition;

        // X ekseninde hareketi uygula
        position.x += moveInput * moveSpeed * Time.deltaTime;

        // X pozisyonunu sýnýrlandýr
        position.x = Mathf.Clamp(position.x, minX, maxX);
        if (moveInput < 0)
        {
            Stick.sprite = sol;
        }
        else if (moveInput > 0)
        {
            Stick.sprite = Sag;
        }
        else 
        {
            Stick.sprite = Orta;
        }
        // Yeni pozisyonu geri uygula
        rectTransform.anchoredPosition = position;
    }
}
