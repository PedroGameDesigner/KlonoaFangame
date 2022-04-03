using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationStateMachine
{
    public class DebugTransitionDisplayer : StateMachineBehaviour
    {
        private string _clipName;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            _clipName = animator.GetCurrentAnimatorClipInfo(layerIndex)[0].clip.name;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            Debug.LogFormat("{0} - Change state from '{1}' to '{2}'",
                animator.gameObject.name,
                _clipName,
                animator.GetNextAnimatorClipInfo(layerIndex)[0].clip.name);
        }
    }
}
