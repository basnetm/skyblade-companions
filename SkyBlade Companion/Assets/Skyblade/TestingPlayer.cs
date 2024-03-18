using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingPlayer : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    private Rigidbody2D rb;

    [Header("Jump")]
    public float jumpSpeed;
    public Transform groundPoint;
    private bool isOnground;
    public LayerMask whatIsGround;

    public bool directionLookEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        isOnground = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

        if (isOnground )
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
            
        }

        // Direction Graphics Handling
        if (directionLookEnabled)
        {
            float newScaleX = Mathf.Sign(rb.velocity.x) * Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
        }
    }
}
