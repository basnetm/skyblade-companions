using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacheteController : MonoBehaviour
{
    public bool IsRotating => _isRotating;

    [Header("Machete Requirements")]
    [SerializeField] private float _detectionRadius = 2f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _rotationSpeed = 300f;
    [SerializeField] private int _damageAmount = 10;
    [SerializeField] private string _rotatingSortingLayer = "Default";
    [SerializeField] private string _nonRotatingSortingLayer = "Rain";

    [Header("Weapon")]
    [SerializeField] private Flamethrower _flamethrower;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private bool _isRotating = false;

    void Start()
    {
        _initialPosition = transform.localPosition;
        _initialRotation = transform.localRotation;
    }

    void Update()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _detectionRadius, _enemyLayer);

        if (hitEnemies.Length > 0)
        {
            if (_flamethrower.IsOnCooldown)
            {
                return;
            }

            RotateMachete();
            _isRotating = true;

            ChangeSortingLayer(_rotatingSortingLayer);
        }
        else
        {
            _isRotating = false;

            ChangeSortingLayer(_nonRotatingSortingLayer);
        }

        if (_isRotating)
        {
            transform.localPosition = new Vector3(0.34f, -0.12f, 0f);
        }
        else
        {
            transform.localPosition = _initialPosition;
            transform.localRotation = _initialRotation;
        }
    }

    private void RotateMachete()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * _rotationSpeed);
    }

    private void ChangeSortingLayer(string sortingLayerName)
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = sortingLayerName;
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
