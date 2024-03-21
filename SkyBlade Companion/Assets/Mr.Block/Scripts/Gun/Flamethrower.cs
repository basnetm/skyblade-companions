using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flamethrower : MonoBehaviour
{
    public bool IsOnCooldown { get { return _isOnCooldown; } }

    public Action OnFlamethrower;

    [Header("Flamethrower Requirements")]
    [SerializeField] private ParticleSystem _flamethrowerParticle;
    [SerializeField] private Transform _firePosition;
    [SerializeField] private GameObject _fireParticlePrefab;
    [SerializeField] private GameObject _flamethrowerCooldownMessage;
    [SerializeField] private float _flamethrowerTime = .6f;
    [SerializeField] private float _flamethrowerCooldown = 1f;
    [SerializeField] private float _flameRange = 8f;

    [Header("Damage Enemy")]
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _intensityIncreaser = 2f;
    [SerializeField] private float _neighborDetectionRange = 0.5f;
    [SerializeField] private int _damageAmount = 1;
    [SerializeField] private float _knockBackThrust = 20f;

    private Vector2 _mousePos;
    private Animator _animator;
    private AudioSource _flamethrowerAudioSource;
    private float _flameTime;
    private Coroutine _flamethrowerCoroutine;
    private Coroutine _flamethrowerCooldownCoroutine;
    private bool _isFlamethrowerActive;
    private bool _isOnCooldown;

    [SerializeField] private PlayerInput _playerInput;
    private FrameInput _frameInput;

    #region Unity Methods

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _flamethrowerAudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        OnFlamethrower += StartFlamethrower;
    }

    private void OnDisable()
    {
        OnFlamethrower -= StartFlamethrower;
    }

    private void Update()
    {
        GatherInput();
        RotateFlamethrower();
        FlamethrowerReady();
    }

    #endregion

    #region Gather Player Input

    private void GatherInput()
    {
        _frameInput = _playerInput.FrameInput;
    }

    #endregion

    #region Flamethrower

    private void RotateFlamethrower()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = BPlayerController.Instance.transform.InverseTransformPoint(_mousePos);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void FlamethrowerReady()
    {
        _isFlamethrowerActive = !_isOnCooldown && _frameInput.IsFlamethrowerActive && _flamethrowerCoroutine == null;

        if (_isFlamethrowerActive)
        {
            OnFlamethrower?.Invoke();
        }
    }

    private void StartFlamethrower()
    {
        if (_flamethrowerCooldownCoroutine != null)
        {
            StopCoroutine(_flamethrowerCooldownCoroutine);
            _flamethrowerCooldownCoroutine = null;
        }

        if (_flamethrowerCoroutine != null)
        {
            StopCoroutine(_flamethrowerCoroutine);
            _flamethrowerCoroutine = null;
        }

        _flamethrowerCoroutine = StartCoroutine(FlamethrowerRoutine());
    }

    private IEnumerator FlamethrowerRoutine()
    {
        if (_isOnCooldown)
        {
            yield break;
        }

        float maxFlameTime = _flamethrowerTime;
        _flameTime = 0f;
        float initialFlameRange = 0f;
        float targetFlameRange = _flameRange;

        ActivateFlamethrowerControls();

        while (_frameInput.IsFlamethrowerActive && _flameTime < maxFlameTime)
        {
            _flameTime += Time.deltaTime;

            float rateOfIncrease = ((targetFlameRange - initialFlameRange) / maxFlameTime) * _intensityIncreaser;
            float currentFlameRange = initialFlameRange + rateOfIncrease * _flameTime;

            EnemyInsight(currentFlameRange);

            yield return null;
        }

        DeactivateFlamethrowerControls();

        _flamethrowerCooldownCoroutine = StartCoroutine(FlamethrowerCooldownRoutine());

        _flamethrowerCoroutine = null;
    }

    private void DeactivateFlamethrowerControls()
    {
        _flamethrowerParticle.Stop();
        _flamethrowerAudioSource?.Stop();
        _animator.SetBool("IsFlameActive", false);
    }

    private void ActivateFlamethrowerControls()
    {
        _animator.SetBool("IsFlameActive", true);
        _flamethrowerParticle.Play();
        _flamethrowerAudioSource?.Play();
    }

    private IEnumerator FlamethrowerCooldownRoutine()
    {
        if (_flamethrowerCooldownCoroutine != null)
        {
            yield break; 
        }

        _isOnCooldown = true;
        _flamethrowerCooldownMessage.SetActive(true);
        yield return new WaitForSeconds(_flamethrowerCooldown);
        _isOnCooldown = false;
        _flamethrowerCooldownMessage.SetActive(false);
        
        _flamethrowerCooldownCoroutine = null;
    }

    #endregion

    #region Enemy Detection and Instantiate Particle

    private void EnemyInsight(float currentFlameRange)
    {
        Vector2 direction = _firePosition.right * _firePosition.localScale.x;
        Vector2 rayOrigin = _firePosition.position;
        float rayLength = currentFlameRange;

        RaycastHit2D[] hits = Physics2D.RaycastAll(rayOrigin, direction, rayLength, _enemyLayer);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (Vector2.Distance(hit.collider.transform.position, _firePosition.position) <= _flameRange)
                {
                    InstantiateFireParticle(hit.collider.gameObject);
                }
            }
        }
    }

    private void InstantiateFireParticle(GameObject targetEnemy)
    {
        ParticleSystem existingParticleSystem = targetEnemy.GetComponentInChildren<ParticleSystem>();
        if (existingParticleSystem != null && existingParticleSystem.GetComponent<FireParticleDamage>() != null)
        {
            return;
        }

        if (Vector2.Distance(targetEnemy.transform.position, _firePosition.position) <= _flameRange)
        {
            GameObject fireParticle = Instantiate(_fireParticlePrefab, targetEnemy.transform.position, Quaternion.identity);

            fireParticle.transform.parent = targetEnemy.transform;

            FireParticleDamage fireParticleDamage = fireParticle.GetComponent<FireParticleDamage>();
            if (fireParticleDamage == null)
            {
                fireParticle.AddComponent<FireParticleDamage>();
            }

            Collider2D[] neighbors = Physics2D.OverlapCircleAll(targetEnemy.transform.position, _neighborDetectionRange, _enemyLayer);
            foreach (Collider2D neighbor in neighbors)
            {
                if (neighbor.gameObject != targetEnemy)
                {
                    InstantiateFireParticle(neighbor.gameObject);
                }
            }
        }
    }

    #endregion

    #region Gizmos

    //private void DrawFlameRay()
    //{
    //    float currentFlameRange = _isFlamethrowerActive ? Mathf.Lerp(0f, _flameRange, _flameTime / _flamethrowerTime) : 0f;

    //    Gizmos.color = Color.red;

    //    Vector2 direction = _firePosition.right * _firePosition.localScale.x;
    //    Vector2 rayOrigin = _firePosition.position;
    //    float rayLength = currentFlameRange;

    //    DrawThickRayGizmos(rayOrigin, direction, rayLength, 0.2f);
    //}

    //private void DrawThickRayGizmos(Vector2 origin, Vector2 direction, float length, float thickness)
    //{
    //    Gizmos.color = Color.red;
    //    float halfThickness = thickness * 0.5f;

    //    Vector2 orthogonalDir = new Vector2(-direction.y, direction.x);

    //    Vector2 lineStart = origin - orthogonalDir * halfThickness;
    //    Vector2 lineEnd = origin + direction * length - orthogonalDir * halfThickness;

    //    for (int i = 0; i < 3; i++)
    //    {
    //        Gizmos.DrawLine(lineStart, lineEnd);
    //        lineStart += orthogonalDir * (thickness / 3f);
    //        lineEnd += orthogonalDir * (thickness / 3f);
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    DrawFlameRay();
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    DrawFlameRay();
    //} 

    #endregion
}
