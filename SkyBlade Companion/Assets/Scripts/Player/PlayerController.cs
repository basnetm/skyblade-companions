using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    public float moveSpeed;

    public float jumpForce;
    public Transform groundPoint;
    private bool isOnground;
    public LayerMask whatIsGround;
    public Animator anim;

    public BulletController shotToFire;
    public Transform shotPoint;

    private bool canDoubleJump;

    public float dashSpeed, dashTime;
    private float dashCounter;

    //dashing effect for player
    public SpriteRenderer sr, afterImage;
    public float afterImageLifetime, timeBetweenAfterImages;
    private float afterImageCounter;
    public Color afterImageColor;

    public float waitAfterDashing;
    private float dashRechargeCounter;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dashRechargeCounter > 0)
        {
            dashRechargeCounter-=Time.deltaTime;
        }
        else
        {
            if (Input.GetButtonDown("Fire2"))
            {
                dashCounter = dashTime;
                ShowAfterImage();
            }
        }

        if (dashCounter > 0)
        {
            dashCounter = dashCounter - Time.deltaTime;
            rb.velocity = new Vector2(dashSpeed * transform.localScale.x, rb.velocity.y);

            afterImageCounter -= Time.deltaTime;
            if(afterImageCounter <= 0)
            {
                ShowAfterImage() ;
            }

            dashRechargeCounter = waitAfterDashing;
        }
        else
        {
            //move sideways
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);

            //handle direction change
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rb.velocity.x > 0)
            {
                transform.localScale = Vector3.one;
            }
        }

        //checking the ground
        isOnground = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

        //jumping
        if (Input.GetButtonDown("Jump") && (isOnground || canDoubleJump))
        {
            if (isOnground)
            {
                canDoubleJump = true;
            }
            else
            {
                canDoubleJump = false;

                anim.SetTrigger("DoubleJump");
            }
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);
            anim.SetTrigger("shotFired");

        }





        anim.SetBool("isOnground", isOnground);
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));

    }
    //dashing effect for player
    public void ShowAfterImage()
    {
       SpriteRenderer image= Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = sr.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;
        
        Destroy(image.gameObject,afterImageLifetime);
        afterImageCounter = timeBetweenAfterImages;
    }
}
