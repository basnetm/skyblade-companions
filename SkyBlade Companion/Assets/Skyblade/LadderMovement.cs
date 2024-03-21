using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
   [SerializeField] private Rigidbody2D rb; // Add this line to declare the Rigidbody2D variable
    private float vertical;
    private float speed = 8f;
    private bool isLadder;
    private bool isClimbing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Add this line to assign the Rigidbody2D component
    }

    void Update()
    {
        vertical = Input.GetAxis("vertical");
        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        else
        {
            rb.gravityScale = 4f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isLadder = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isLadder = false;
        isClimbing = false;
    }
}
