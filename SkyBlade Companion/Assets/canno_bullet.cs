using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class canno_bullet : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
      rb=GetComponent<Rigidbody2D>();
         rb.velocity = -transform.right * speed;
        //transform.Translate(Vector2.left * speed * Time.deltaTime);
        Destroy(gameObject,4f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
