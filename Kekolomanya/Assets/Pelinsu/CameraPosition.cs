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
    public float smoothSpeed = 5f; // Kameranýn hareket yumuþaklýðý
    public float leftPosition = 3.25f; // Kameranýn x eksenindeki sol pozisyonu
    public float rightPosition = 19f; // Kameranýn x eksenindeki sað pozisyonu
    public float threshold = 1f; // Karakterin kameradan çýkmasý için sýnýr mesafesi
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

            // Eðer karakter sað sýnýrý geçerse
            if (characterX > cameraX + threshold && cameraX != rightPosition)
            {
                // Kamerayý sað konuma taþý
                MoveCamera(rightPosition);
            }
            // Eðer karakter sol sýnýrý geçerse
            else if (characterX < cameraX - threshold && cameraX != leftPosition)
            {
                // Kamerayý sol konuma taþý
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
