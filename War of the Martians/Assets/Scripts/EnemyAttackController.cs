using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    public float attackDamage = 10f;
    public float attackSpeed = 2f;
    public float attackRange = 2f;
    public Transform targetToAttack;

    private bool attackOnCooldown = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && targetToAttack == null) {
            //Debug.Log("Detected: " + other.transform.name);
            targetToAttack = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") && targetToAttack != null)
            targetToAttack = null;
    }

    public void Attack() {
        UnitController unit = gameObject.GetComponent<UnitController>();

        IEnumerator att() {
            // do damage
            Debug.Log("Attacking");
            Debug.Log(targetToAttack.gameObject);
            targetToAttack.gameObject.GetComponent<UnitController>().Damage(attackDamage);

            // Attack Speed
            yield return new WaitForSeconds(attackSpeed);
            attackOnCooldown = false;
        }
        if (attackOnCooldown == false) {
            attackOnCooldown = true;
            StartCoroutine(att());
        }
    }
}
