using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class LedgeHang : StateMachineBehaviour
    {

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Vector3 newPos = PlayerController.instance.playerModel.transform.position;
            
            newPos.y = PlayerController.instance.calculatedClimbPoint.y - 1f;
            
            PlayerController.instance.gameObject.transform.position = newPos;
        }

      

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerController.instance.anim.SetTrigger("ClimbUpFromLedge");
                StopDetectingLedge();
            }

            if (Input.GetAxisRaw("Vertical") < -0.01)
            {
                PlayerController.instance.FallFromLedge();
            }

            
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }


        public void StopDetectingLedge()
        {
            Debug.Log("Trying to climb up from ledge");
            PlayerController.instance.anim.SetBool("Hanging", false);
            PlayerController.instance.wallDetector.gameObject.SetActive(false);
            PlayerController.instance.ledgeDetector.gameObject.SetActive(false);

        }

    }

}
