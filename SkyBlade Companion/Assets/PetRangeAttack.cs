using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetRangeAttack : MonoBehaviour
{
    [Header("Range Attack Requirements")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float attackRange = 6f;
    public float projectileSpeed = 10f;
    public float rangedCooldown = 2f;
    public LayerMask enemyLayer;

    private Animator anim;
    private PetController petController;
    private bool isAttacking = false;

    private Coroutine rangeAttackCoroutine;

    private PetMeleeAttack petMeleeAttack;

    public bool isRangeAttacking = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        petMeleeAttack = GetComponent<PetMeleeAttack>();
        petController = GetComponent<PetController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking && !petMeleeAttack.isMeleeAttacking)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

            if (hitEnemies.Length > 0)
            {
                isRangeAttacking = true;

                petController.followEnabled = false;

                foreach (Collider2D enemyCollider in hitEnemies)
                {
                    //check if the enemy is already dead
                    if (enemyCollider == null || !enemyCollider.gameObject.activeSelf)
                    {
                        continue;
                    }

                    rangeAttackCoroutine = StartCoroutine(RangedAttackCo());
                }
            }
            else
            {
                StopRangeAttack();
            }
        }
    }

    private IEnumerator RangedAttackCo()
    {
        isAttacking = true;
        anim.SetBool("IsRangeAttack", true);

        yield return new WaitForSeconds(rangedCooldown);

        isAttacking = false;
    }

    public void SpawnProjectile()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    public void StopRangeAttack()
    {
        if (rangeAttackCoroutine != null)
        {
            anim.SetBool("IsRangeAttack", false);
            StopCoroutine(rangeAttackCoroutine);
            isAttacking = false;
            isRangeAttacking = false;
            petController.followEnabled = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
