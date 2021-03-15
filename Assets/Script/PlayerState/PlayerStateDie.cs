using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDie : PlayerStateBase
{
    //public PlayerControl pc;
    public override void EnterState(PlayerControl control)
    {
        control.StartCoroutine(control.BlowUp());
        control.playerAnim.SetBool("isUp",false);
        control.playerAnim.SetBool("isDown",false);
        control.playerAnim.SetBool("isLeft",false);
        control. playerAnim.SetBool("isRight",false);
    }

    public override void Update(PlayerControl control)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            control.aC = control.spawn;
            control.aS.Play();
            control.respawn();
        }
    }

    public override void LeaveState(PlayerControl control)
    {

    }
}
    
