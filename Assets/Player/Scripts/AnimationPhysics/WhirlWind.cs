using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlWind : StateMachineBehaviour
{
    JWPlayerController player;
    Quaternion originalRot;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<JWPlayerController>();

        originalRot = player.transform.rotation;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player.transform.rotation = originalRot;

        if (player.effectTrigger)
        {
            player.skillVFXs[1].SetActive(true);
            player.animator.SetBool("WhirlWind", false);
        }

        if(player.animTrigger)
        {
            Debug.Log("Exit");
            player.skillVFXs[1].SetActive(false);
        }

        if (Input.GetMouseButton(1))
        {
            player.nav.SetDestination(player.targetPosition);
        }
    }

}