using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerCollectionState : StateMachineBehaviour
{
    private WorkerUnitController workerController;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Debug.Log("Worker entering Collection");
        workerController = animator.gameObject.GetComponent<WorkerUnitController>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Q: What if there is no more resource left?
        // A: Go to idle and clear appropriate values
        if (workerController.GetTarget() == null) {
            animator.SetBool("isCollecting", false);
            workerController.Collect(null);
            return;

        // Q: When should go back to traveling?
        // A: When resource has been harvested or worker is out of collection range
        } else if (!workerController.HasResourceInHand()) {
            // Confirm that worker is in collection range
            float distanceToTarget = Vector2.Distance(workerController.GetTarget().position, workerController.transform.position);
            if (distanceToTarget <= workerController.collectionDistance) {
                // Will wait for worker to collect

                workerController.CollectResource();
                // TODO: add another state where collection animation is started and collectTime is respected

                animator.SetBool("isCollecting", false);
                animator.SetBool("isTraveling", true);
                return;
            } else {
                animator.SetBool("isCollecting", false);
                animator.SetBool("isTraveling", true);
                return;
            }
        }

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //    Debug.Log("Worker leaving Collection");
    //}

    //OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //   // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
