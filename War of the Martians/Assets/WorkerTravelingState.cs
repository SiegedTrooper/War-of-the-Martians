using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerTravelingState : StateMachineBehaviour
{
    private WorkerUnitController workerController;
    private UnitController unitController;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("Worker enter traveling");
        workerController = animator.transform.GetComponent<WorkerUnitController>();
        unitController = animator.transform.GetComponent<UnitController>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Q: When should return back to Idle?
        // A: When resourceTarget nulls and has no target in hand
        if (workerController.GetTarget() == null && workerController.HasResourceInHand() == false) {
            animator.SetBool("isTraveling", false);
            animator.SetBool("isDepositing", false);
            animator.SetBool("isCollecting", false);
            return;
        }

        // Q: When should transition to Deposit?
        // A: When has resource in hand
        if (workerController.HasResourceInHand()) {
            if (HQGlobal.instance.allHQs.Count > 0) {
                if (workerController.GetHQ() == null) {
                    workerController.SetHQ(HQGlobal.instance.GetNearestHQ(animator.transform));
                }
                GameObject hq = workerController.GetHQ();

                // Checking to see if worker can deposit
                float distanceToTarget = Vector2.Distance(hq.transform.position, workerController.transform.position);
                if (distanceToTarget <= 2f) { // Transitioning to Deposit
                    animator.SetBool("isTraveling", false);
                    animator.SetBool("isDepositing", true);
                    return;
                } else { // Continue to move to hq
                    Debug.Log("Moving to hq");
                    unitController.SpecialTargetFunction();
                }
            } else {
                Debug.Log("Worker cannot deposit - All HQs may be destroyed");
                // Safe Defaulting to Idle
                animator.SetBool("isTraveling", false);
                animator.SetBool("isCollecting", false);
                animator.SetBool("isDepositing", false);
                return;
            }
        }

        // Q: When should transition to Collect?
        // A: No resource in hand and in collection range
        else {
            float distanceToTarget = Vector2.Distance(workerController.GetTarget().position, workerController.transform.position);
            if (distanceToTarget <= workerController.collectionDistance) {
                animator.SetBool("isTraveling", false);
                animator.SetBool("isCollecting", true);
                return;
            } else {
                unitController.SpecialTargetFunction();
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log("Worker left traveling");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
