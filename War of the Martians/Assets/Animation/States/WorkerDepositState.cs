using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerDepositState : StateMachineBehaviour
{
    private WorkerUnitController workerController;
    private UnitController unitController;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Debug.Log("Worker enter deposit");
        workerController = animator.transform.GetComponent<WorkerUnitController>();
        unitController = animator.transform.GetComponent<UnitController>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
        if (workerController.HasResourceInHand()) {
            // Depositing resources
            workerController.RemoveResourceInHand();
            PlayerResources.instance.AddResource(workerController.collectionAmount,workerController.GetResourceType());
            // next update will set behavior
        } else {
            // Q: When should I go back to traveling?
            // A: When resource is no longer in hand

            animator.SetBool("isDepositing", false);
            // Q: When should I go to Idle?
            // A: If resource no longer exists // new condition to implement: and when there are no nearby resources
            // TODO: Implement new condition
            if (workerController.GetTarget() == null) {
                animator.SetBool("isTraveling", false);
            } else {
                animator.SetBool("isTraveling", true);
            }
            return;
        }



        // Q: Edge-case: What if HQ no longer exists?
        // A: Find the next nearest HQ - if that fails, go to idle
        // TODO: IMPLEMENT
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //    Debug.Log("Worker left deposit");
    //}

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
