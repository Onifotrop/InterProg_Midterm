using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAlive : PlayerStateBase
{
    public override void EnterState(PlayerControl control)
    {
        control.animator.Play("idle_D" , -1,  0f);
    }

    public override void Update(PlayerControl control)
    {
        if(control.alive)
        {
            control.dashWithX();
            control.moveWithWASD();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            control.ChangeState(control.stateDie);
        }
    }

    public override void LeaveState(PlayerControl control)
    {
        
    }
}
