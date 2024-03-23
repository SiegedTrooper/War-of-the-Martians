using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform targetToAttack;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy") && targetToAttack == null) {
            Debug.Log("Detected: " + other.transform.name);
            targetToAttack = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Enemy") && targetToAttack != null)
            targetToAttack = null;
    }
}
