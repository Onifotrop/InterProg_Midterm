using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAlive : PlayerStateBase
{
    //This is what happens when player are alive.
    public override void EnterState(PlayerControl control)
    {
        //This makes everytime the play respawn go back to idle state as well, I give credit to the Unity animator.Play forum
        control.animator.Play("idle_D" , -1,  0f);
    }

    public override void Update(PlayerControl control)
    {
        if(control.alive)
        {
            //basic control
            control.dashWithX();
            control.moveWithWASD();
        }
    }

    public override void LeaveState(PlayerControl control)
    {
        
    }
}
