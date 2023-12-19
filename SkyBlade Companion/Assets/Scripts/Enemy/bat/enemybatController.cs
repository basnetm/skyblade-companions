
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemybatController : MonoBehaviour
{
    public float rangeToStartChase;
    private bool isChasing;
    public float moveSpeed, turnSpeed;
    private Transform player;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = playerhealthController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChasing)
        {
            if (Vector3.Distance(transform.position, player.position) < rangeToStartChase)
            {
                isChasing = true;
                anim.SetBool("isChasing", isChasing);
            }
        }
        else
        {
            if (player.gameObject.activeSelf)
            {
                Vector3 direction = transform.position - player.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                //Quaternion targetRot = Quaternion.Euler(0, 0, angle);
                Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);

                // Move the enemy towards the player
                //transform.position = Vector3.MoveTowards(transform.position, player.position,  moveSpeed* Time.deltaTime );
                transform.position += -transform.right * moveSpeed * Time.deltaTime;
            }
        }
    }
}



//yo ishow dai ko
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class enemybatController : MonoBehaviour
//{
//    public Transform[] points;
//    public float moveSpeed;
//    private int currentPoint;

//    public SpriteRenderer sr;

//    public float distanceToAttackPlayer, chaseSpeed;

//    private Vector3 attackTarget;


//    public float waitAfterAttack;
//    private float attackCounter;

//    // Start is called before the first frame update
//    void Start()
//    {
//        for (int i = 0; i < points.Length; i++)
//        {
//            points[i].parent = null;
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (attackCounter > 0)
//        {
//            attackCounter -= Time.deltaTime;
//        }
//        else
//        {



//            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToAttackPlayer)
//            {

//                attackTarget = Vector3.zero;

//                transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

//                if (Vector3.Distance(transform.position, points[currentPoint].position) < 0.5f)
//                {
//                    currentPoint++;

//                    if (currentPoint >= points.Length)
//                    {
//                        currentPoint = 0;
//                    }
//                }

//                if (transform.position.x < points[currentPoint].position.x)
//                {
//                    sr.flipX = true;
//                }
//                else if (transform.position.x > points[currentPoint].position.x)
//                {
//                    sr.flipX = false;
//                }

//            }
//            else
//            {
//                //attacking player

//                if (attackTarget == Vector3.zero)
//                {
//                    attackTarget = PlayerController.instance.transform.position;
//                }

//                transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

//                if (Vector3.Distance(transform.position, attackTarget) <= .1f)
//                {

//                    attackCounter = waitAfterAttack;
//                    attackTarget = Vector3.zero;
//                }
//            }
//        }

//    }

//}

