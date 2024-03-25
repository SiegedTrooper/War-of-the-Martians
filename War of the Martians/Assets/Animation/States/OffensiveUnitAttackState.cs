using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffensiveUnitAttackState : StateMachineBehaviour
{
    AttackController attackerController;
    UnitController unitController;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        attackerController = animator.transform.GetComponent<AttackController>();
        unitController = animator.transform.GetComponent<UnitController>();
        //Debug.Log(unitController.gameObject.name + " has entered the attack state");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // if unit dies, stop
        if (unitController == null) { // Unit is dead, this script will shortly die
            return;
        }

        // if enemy dies or out of detection, exit
        if (attackerController.targetToAttack == null) {
            animator.SetBool("isAttacking",false);
            animator.SetBool("isFollowing", false);
            animator.SetBool("isIdle", true);
            return;
        }
        
        // check if enemy is in range
        float distanceToTarget = Vector2.Distance(attackerController.targetToAttack.position, animator.transform.position);
        if (distanceToTarget > attackerController.attackRange) { // Enemy is outside of attack range
            animator.SetBool("isAttacking", false);
            animator.SetBool("isFollowing", true);
        } else {
            attackerController.Attack();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Debug.Log(unitController.gameObject.name + " has left the attack state");
    }
}
