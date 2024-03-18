using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPlayerController : MonoBehaviour
{
    public Vector2 MoveInput => _frameInput.Move;

    public static Action OnJump;
    public static Action OnLand;
    public static Action OnJetpack;

    public static BPlayerController Instance;

    [SerializeField] private ParticleSystem _jetpackParticle;
    //[SerializeField] private TrailRenderer _jetpackTrailRenderer;
    [SerializeField] private Transform _feetTransform;
    [SerializeField] private Vector2 _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _jumpStrength = 7f;
    [SerializeField] private float _extraGravity = 700f;
    [SerializeField] private float _gravityDelay = 0.2f;
    [SerializeField] private float _coyoteTime = .5f;
    [SerializeField] private float _jetpackTime = .6f;
    [SerializeField] private float _jetpackStrength = 11f;
    [SerializeField] private float _maxFallSpeedVelocity = -20f;
    [SerializeField] private Animator _animator;

    private bool _wasGroundedLastFrame;
    private float _timeInAir, _coyoteTimer;
    private bool _doubleJumpAvailable;
    private Coroutine _jetpackCoroutine;

    private PlayerInput _playerInput;
    private FrameInput _frameInput;

    private Rigidbody2D _rigidBody;
    private Movement _movement;

    #region Unity Methods

    public void Awake()
    {
        if (Instance == null) { Instance = this; }

        _rigidBody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _movement = GetComponent<Movement>();
    }

    private void OnEnable()
    {
        OnJump += ApplyJumpForce;
        OnLand += PlayLandingAnimation;
        OnJetpack += StartJetpack;
    }

    private void OnDisable()
    {
        OnJump -= ApplyJumpForce;
        OnLand -= PlayLandingAnimation;
        OnJetpack -= StartJetpack;
    }

    private void Update()
    {
        GatherInput();
        Movement();
        CoyoteTimer();
        HandleJump();
        HandleSpriteFlip();
        GravityDelay();
        Jetpack();
    }

    private void FixedUpdate()
    {
        ExtraGravity();
    }

    private void OnDestroy()
    {
        Fade fade = FindFirstObjectByType<Fade>();
        fade?.FadeInAndOut();
    }

    #endregion

    #region Check Ground

    public bool CheckGrounded()
    {
        Collider2D isGrounded = Physics2D.OverlapBox(_feetTransform.position, _groundCheck, 0f, _groundLayer);

        if (isGrounded && !_wasGroundedLastFrame && isGrounded.tag != "Platform")
        {
            OnLand?.Invoke();
        }

        _wasGroundedLastFrame = isGrounded;

        return isGrounded;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_feetTransform.position, _groundCheck);
    }

    #endregion

    #region Gravity

    private void GravityDelay()
    {
        if (!CheckGrounded())
        {
            _timeInAir += Time.deltaTime;
        }
        else
        {
            _timeInAir = 0f;
        }
    }

    private void ExtraGravity()
    {
        if (_timeInAir > _gravityDelay)
        {
            _rigidBody.AddForce(new Vector3(0f, -_extraGravity * Time.deltaTime));
            if (_rigidBody.velocity.y < _maxFallSpeedVelocity)
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _maxFallSpeedVelocity);
            }
        }
    }

    #endregion

    #region Move and Jump

    private void GatherInput()
    {
        _frameInput = _playerInput.FrameInput;
    }

    private void Movement()
    {
        _movement.SetCurrentDirection(_frameInput.Move.x);
    }

    private void HandleJump()
    {
        if (!_frameInput.Jump)
        {
            return;
        }

        if (CheckGrounded())
        {
            OnJump?.Invoke();
        }
        else if (_coyoteTimer > 0f)
        {
            OnJump?.Invoke();
        }
        else if (_doubleJumpAvailable)
        {
            _doubleJumpAvailable = false;
            OnJump?.Invoke();
        }
    }

    private void CoyoteTimer()
    {
        if (CheckGrounded())
        {
            _coyoteTimer = _coyoteTime;
            _doubleJumpAvailable = true;
        }
        else
        {
            _coyoteTimer -= Time.deltaTime;
        }
    }

    private void ApplyJumpForce()
    {
        _rigidBody.velocity = Vector2.zero;
        _timeInAir = 0f;
        _coyoteTimer = 0f;
        _rigidBody.AddForce(Vector2.up * _jumpStrength, ForceMode2D.Impulse);


    }

    private void PlayLandingAnimation()
    {
        _animator.SetTrigger("Land");
    }

    #endregion

    #region Jetpack

    private void Jetpack()
    {
        if (!_frameInput.Jetpack || _jetpackCoroutine != null)
        {
            return;
        }

        OnJetpack?.Invoke();
    }

    private void StartJetpack()
    {
        _jetpackParticle.Play();
        _jetpackCoroutine = StartCoroutine(JetpackRoutine());
    }

    private IEnumerator JetpackRoutine()
    {
        float jetTime = 0f;

        while (jetTime < _jetpackTime)
        {
            jetTime += Time.deltaTime;
            _rigidBody.velocity = Vector2.up * _jetpackStrength;

            yield return null;
        }

        _jetpackParticle.Stop();
        _jetpackCoroutine = null;
    }

    #endregion

    #region Character Flip

    public bool IsFacingRight()
    {
        return transform.eulerAngles.y == 0;
    }

    private void HandleSpriteFlip()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
    }

    #endregion
}
