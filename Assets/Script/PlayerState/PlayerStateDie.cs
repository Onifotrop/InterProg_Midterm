using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateDie : PlayerStateBase
{
    //This is what happens when player die.
    public override void EnterState(PlayerControl control)
    {
        //To set the bool of the animator, so everytime player respawn, it go back to idle state
        control.StartCoroutine(control.BlowUp());
        control.playerAnim.SetBool("isUp",false);
        control.playerAnim.SetBool("isDown",false);
        control.playerAnim.SetBool("isLeft",false);
        control. playerAnim.SetBool("isRight",false);
    }

    public override void Update(PlayerControl control)
    {
        //Press R to respawn
        if (Input.GetKeyDown(KeyCode.R))
        {
            control.respawn();
        }
    }

    public override void LeaveState(PlayerControl control)
    {

    }
}
    
