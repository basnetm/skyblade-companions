using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMeleeAttack : MonoBehaviour
{
    [Header("Meleee Requirements")]
    public int damageAmount = 10;
    public float meleeRange = 2f;
    public float meleeCooldown = 1f;
    public LayerMask enemyLayer;

    private bool isAttacking = false;
    private PetController petController;
    private Animator anim;

    private Coroutine meleeAttackCoroutine;

    private PetRangeAttack petRangeAttack;
    public bool isMeleeAttacking = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        petController = GetComponent<PetController>();
        petRangeAttack = GetComponent<PetRangeAttack>();    
    }

    private void Update()
    {
        if (!isAttacking && !petRangeAttack.isRangeAttacking)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, meleeRange, enemyLayer);

            if (hitEnemies.Length > 0)
            {
                isMeleeAttacking = true;

                petController.followEnabled = false;

                foreach (Collider2D enemyCollider in hitEnemies)
                {
                    //check if the enemy is already dead
                    if (enemyCollider == null || !enemyCollider.gameObject.activeSelf)
                    {
                        continue;
                    }

                    // perform melee attack
                    meleeAttackCoroutine = StartCoroutine(MeleeAttackCO());

                    //give damage to enemy
                    enemyhealthController enemyHealth = enemyCollider.GetComponent<enemyhealthController>();

                    if (enemyHealth != null)
                    {
                        enemyHealth.DamageEnemy(damageAmount);
                    }
                }
            }
            else
            {
                Debug.Log(hitEnemies);
                StopMeleeAttack();
            }
        }
    }

    private IEnumerator MeleeAttackCO()
    {
        isAttacking = true;
        anim.SetBool("IsMeleeAttack", true);

        yield return new WaitForSeconds(meleeCooldown);

        isAttacking = false;
    }

    public void StopMeleeAttack()
    {
        if (meleeAttackCoroutine != null)
        {
            anim.SetBool("IsMeleeAttack", false);
            StopCoroutine(meleeAttackCoroutine);
            isAttacking = false;
            isMeleeAttacking = false;
            petController.followEnabled = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }
}

