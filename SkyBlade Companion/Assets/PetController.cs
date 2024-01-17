using Pathfinding;
using System.Collections;
using UnityEngine;

public class PetController : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f, jumpForce = 100f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true, isJumping, isInAir;
    public bool directionLookEnabled = true;

    //[SerializeField] Vector3 startOffset;

    private Path path;
    private int currentWaypoint = 0;
    [SerializeField] public bool isGrounded;
    Seeker seeker;
    Rigidbody2D rb;
    private Animator anim;
    private bool isOnCoolDown;

    //public Collider2D petCollider;
    public Transform groundPoint;
    public LayerMask whatIsGround;

    private bool isAtTarget = false;
    public float stopDuration = 2f;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isJumping = false;
        isInAir = false;
        isOnCoolDown = false;

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        if (followEnabled)
        {
            if (TargetInDistance())
            {
                if (!isAtTarget)
                {
                    PathFollow();
                    UpdateAnimations();
                }
                else
                {
                    // Pet is at the target, stop for a moment
                    rb.velocity = Vector2.zero;
                    StartCoroutine(StopAtTarget());
                }
            }
            else
            {
                isAtTarget = false; // Reset the flag when the target is not in distance
            }
        }
        else if(!isAtTarget)
        {
            rb.velocity = Vector2.zero; // only reset velocity when not at the target
        }
    }

    private IEnumerator StopAtTarget()
    {
        
        yield return new WaitForSeconds(stopDuration);
        isAtTarget = false; //continue following the target after the stop duration
        
    }

    // Animations
    private void UpdateAnimations()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        // Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            isAtTarget = true;
            return;
        }

        isGrounded = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed;

        // Jump
        if (jumpEnabled && isGrounded && !isInAir && !isOnCoolDown)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                if (isInAir) return;
                isJumping = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                StartCoroutine(JumpCoolDown());
            }
        }
        if (isGrounded)
        {
            isJumping = false;
            isInAir = false;
        }
        else
        {
            isInAir = true;
        }

        // Movement
        rb.velocity = new Vector2(force.x, rb.velocity.y);

        // Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Direction Graphics Handling
        if (directionLookEnabled)
        {
            float newScaleX = Mathf.Sign(rb.velocity.x) * Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    IEnumerator JumpCoolDown()
    {
        isOnCoolDown = true;
        yield return new WaitForSeconds(1f);
        isOnCoolDown = false;
    }
}
