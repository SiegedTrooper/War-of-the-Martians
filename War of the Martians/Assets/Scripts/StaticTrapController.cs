using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTrapController : MonoBehaviour
{
    [SerializeField] private float trapDamage = 200f;

    private void OnCollisionEnter2D(Collision2D other) {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Player")) {
            if (other.gameObject.GetComponent<UnitController>()) {
                other.gameObject.GetComponent<UnitController>().Damage(trapDamage);
                Destroy(gameObject);
            }
        }
    }

    // Opted to use the above method
    /*
    private void OnTriggerEnter2D(Collider2D other) {
        float distanceToTarget = Vector2.Distance(other.transform.position, transform.position);
        if (other.CompareTag("Player") && distanceToTarget <= 2f) {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
    */
}
