using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack : MonoBehaviour
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

    private void Start()
    {
        anim = GetComponent<Animator>();
        petController = GetComponent<PetController>();
    }

    private void Update()
    {
        if (!isAttacking)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, meleeRange, enemyLayer);

            if (hitEnemies.Length > 0)
            {
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
        anim.SetBool("IsMeleeAttack", false);
    }

    public void StopMeleeAttack()
    {
        if (meleeAttackCoroutine != null)
        {
            StopCoroutine(meleeAttackCoroutine);
            isAttacking = false;
            anim.SetBool("IsMeleeAttack", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }
}
