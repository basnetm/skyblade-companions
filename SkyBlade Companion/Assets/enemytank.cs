using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemytank : MonoBehaviour
{
    public Transform gunpoint1;
    public Transform gunpoint2;
    public GameObject enemybullet;
    public float enemybulletspawntime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(enemyshooting());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void enemyfire()
    {
        GameObject bullet1 = Instantiate(enemybullet, gunpoint1.position, Quaternion.identity);
        GameObject bullet2 = Instantiate(enemybullet, gunpoint2.position, Quaternion.identity);

        // Check if bullets go out of the scene
        CheckBulletOffScreen(bullet1);
        CheckBulletOffScreen(bullet2);
    }

    IEnumerator enemyshooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(enemybulletspawntime);
            enemyfire();
        }
    }

    void CheckBulletOffScreen(GameObject bullet)
    {
        Camera mainCamera = Camera.main;

        // Get the viewport position of the bullet
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(bullet.transform.position);

        // If the bullet is outside the viewport, destroy it
        if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
        {
            Destroy(bullet);
        }
    }
}
