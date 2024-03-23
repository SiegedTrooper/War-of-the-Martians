using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFollowState : StateMachineBehaviour
{
    AttackController attackerController;
    UnitController unitController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        attackerController = animator.transform.GetComponent<AttackController>();
        unitController = animator.transform.GetComponent<UnitController>();
        //Debug.Log(unitController.gameObject.name + " has entered the follow state");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Should unit transition to idle state?
        if (attackerController.targetToAttack == null) {
            animator.SetBool("isFollowing", false);
            animator.SetBool("isIdle", true);
        } else {
            if (unitController.isCommandedToMove == false) {
                // Moving unit towards enemy
                Debug.Log("Attack sent");
                unitController.targetPosition = (attackerController.targetToAttack.position);
                unitController.SpecialTargetFunction();
            }

            // Should unit transition to attack state?
            float distanceToTarget = Vector2.Distance(attackerController.targetToAttack.position, animator.transform.position);
            if (distanceToTarget <= attackerController.attackRange)
                animator.SetBool("isFollowing", false);
                animator.SetBool("isAttacking", true);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        unitController.targetPosition = animator.transform.position; // Stops the unit to attack the enemy
        //Debug.Log(unitController.gameObject.name + " has left the follow state");
    }
}
