using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 1;
    [SerializeField] private float _knockBackThrust = 20f;


    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == LayerMask.NameToLayer("Enemies"))
        {
            Debug.Log("Hitted");

            IHitable iHitable = other.gameObject.GetComponent<IHitable>();
            iHitable?.TakeHit();

            IDamageable iDamageable = other.gameObject.GetComponent<IDamageable>();
            iDamageable?.TakeDamage(BPlayerController.Instance.transform.position, _damageAmount, _knockBackThrust); 
        }
    }
}
