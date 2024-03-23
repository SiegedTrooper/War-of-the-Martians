using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIdleState : StateMachineBehaviour
{
    AttackController attackerController;
    UnitController unitController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        attackerController = animator.transform.GetComponent<AttackController>();
        unitController = animator.transform.GetComponent<UnitController>();
        //Debug.Log(unitController.gameObject.name + " has entered the idle state");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Looking for enemies nearby
        if (attackerController.targetToAttack != null) {
            // Following target -- Transitioning to following state
            animator.SetBool("isFollowing", true);
            animator.SetBool("isIdle", false);
        }
    }
    
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Debug.Log(unitController.gameObject.name + " has left the idle state");
    }
}
