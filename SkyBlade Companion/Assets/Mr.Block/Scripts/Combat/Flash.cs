using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _whiteFlashMaterial;
    [SerializeField] private float _flashTime = 0.1f;

    private SpriteRenderer[] _spriteRenderers;

    //private SpriteRenderer _spriteRenderer;
    //public SpriteRenderer spriteRenderer;
    //private Material currentMaterial;
    //private ColorChanger _colorChanger;

    private void Awake()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        //_spriteRenderer = GetComponent<SpriteRenderer>();

        
        
        //_colorChanger = GetComponent<ColorChanger>();
    }

    public void StartFlash()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        foreach (var sr in _spriteRenderers)
        {
            sr.material = _whiteFlashMaterial;

            //if (_colorChanger)
            //{
            //    _colorChanger.SetColor(Color.white);
            //}
        }

        //currentMaterial = spriteRenderer.material;

        //spriteRenderer.material = _whiteFlashMaterial;

        yield return new WaitForSeconds(_flashTime);

        SetDefaultMaterial();
    }

    private void SetDefaultMaterial()
    {
        foreach (var sr in _spriteRenderers)
        {
            sr.material = _defaultMaterial;

            //if (_colorChanger)
            //{
            //    _colorChanger.SetColor(_colorChanger.DefaultColor);
            //}
        }

        //spriteRenderer.material = currentMaterial;
    }
}
