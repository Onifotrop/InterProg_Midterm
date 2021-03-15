using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateBase
{
    public abstract void EnterState(PlayerControl control);
    
    public abstract void Update(PlayerControl control);
    
    public abstract void LeaveState(PlayerControl control);
}
