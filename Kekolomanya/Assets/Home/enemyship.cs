using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyship : MonoBehaviour
{
    public GameObject EnemyBullet;
    public RectTransform spawnPoint;
    public RectTransform rectTransform;
    IEnumerator randomAttack() 
    {
        yield return new WaitForSeconds(Random.Range(0, 5));
        // Instantiate the bullet
        GameObject bullet = Instantiate(EnemyBullet, this.transform.position, Quaternion.identity);

        // Set the parent to be the Canvas (the parent of the spawnPoint)
        bullet.transform.SetParent(spawnPoint.parent, false);

        // Get the RectTransform of the bullet to adjust the position
        RectTransform bulletRectTransform = bullet.GetComponent<RectTransform>();

        // Position the bullet in front of the ship
        bulletRectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + 50);
    }
    
}
