using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretgun : MonoBehaviour
{
  
    public float speed = 5f;
    public float lifetime = 2f;

    void Start()
    {
        // Destroy bullet after lifetime
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move bullet forward
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy bullet on collision
        Destroy(gameObject);
    }
}
