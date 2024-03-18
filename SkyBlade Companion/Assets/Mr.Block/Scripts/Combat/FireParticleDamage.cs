using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticleDamage : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 4f;
    [SerializeField] private float damageRadius = 2f;
    [SerializeField] private int _damageAmount = 1;
    [SerializeField] private float _knockBackThrust = 20f;

    private void Start()
    {
        StartCoroutine(ApplyDamageCoroutine());
    }

    private void DestroyAfterTime()
    {
        Destroy(gameObject, _lifeTime);
    }

    private IEnumerator ApplyDamageCoroutine()
    {
        float startTime = Time.time;

        while (Time.time - startTime < _lifeTime)
        {
            float elapsed = Time.time - startTime;
            float damageRatio = Mathf.Clamp01(elapsed / _lifeTime);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageRadius);

            foreach (Collider2D collider in colliders)
            {
                IHitable iHitable = collider.gameObject.GetComponent<IHitable>();
                iHitable?.TakeHit();

                IDamageable iDamageable = collider.gameObject.GetComponent<IDamageable>();
                if (iDamageable != null)
                {
                    int incrementalDamage = Mathf.RoundToInt(_damageAmount * damageRatio);
                    iDamageable.TakeDamage(BPlayerController.Instance.transform.position, incrementalDamage, _knockBackThrust);
                }
            }

            yield return null;
        }

        DestroyAfterTime();
    }
}

