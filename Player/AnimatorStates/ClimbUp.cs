﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DillonsDream
{
    public class ClimbUp : StateMachineBehaviour
    {
       
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerController.instance.anim.SetBool("ClimbUp", false);
        }



        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {


        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerController.instance.anim.SetBool("ClimbUp", true);
        }

       
    }

}