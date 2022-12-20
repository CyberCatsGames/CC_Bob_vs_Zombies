using UnityEngine;

namespace Suriyun.MobileTPS
{
    public class SentEvent : StateMachineBehaviour
    {
        private static Agent agent;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (agent == null)
            {
                agent = animator.GetComponent<Agent>();
            }

            // Sync mecanime state with outside variables //s
            if (stateInfo.IsName("hide"))
            {
                agent.GameCamera.zoomed = false;
            }
            else if (stateInfo.IsName("run"))
            {
                agent.GameCamera.zoomed = false;
            }
            else if (stateInfo.IsName("shoot"))
            {
                agent.GameCamera.zoomed = true;
            }
        }
    }
}