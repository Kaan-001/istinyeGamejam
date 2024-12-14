using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class CameraPosition : MonoBehaviour
{
    public static int FighterCount;
    public Transform character; // Takip edilecek karakterin Transform'u
    public float smoothSpeed = 5f; // Kameran�n hareket yumu�akl���
    public float leftPosition = 3.25f; // Kameran�n x eksenindeki sol pozisyonu
    public float rightPosition = 19f; // Kameran�n x eksenindeki sa� pozisyonu
    public float threshold = 1f; // Karakterin kameradan ��kmas� i�in s�n�r mesafesi
    public TMP_Text PickText;
    public static bool Door = false;
   
    void LateUpdate()
    {
        PickText.text = "Pick Your Fighters!\n"+FighterCount+"/2";
        if(FighterCount == 2 && Door ) 
        {
            HaritaArea.Leveller = new bool[4];
            HaritaArea.Leveller[0] = true;
            HaritaArea.Leveller[1] = true;
            HaritaArea.Leveller[2] = true;
            SceneManager.LoadScene("HaritaSahne");

        }
        if (character != null)
        {
            float characterX = character.position.x;
            float cameraX = transform.position.x;

            // E�er karakter sa� s�n�r� ge�erse
            if (characterX > cameraX + threshold && cameraX != rightPosition)
            {
                // Kameray� sa� konuma ta��
                MoveCamera(rightPosition);
            }
            // E�er karakter sol s�n�r� ge�erse
            else if (characterX < cameraX - threshold && cameraX != leftPosition)
            {
                // Kameray� sol konuma ta��
                MoveCamera(leftPosition);
            }
        }
    }

    void MoveCamera(float targetX)
    {
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
