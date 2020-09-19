using System;
using System.Collections.Generic;
using System.Text;
using Nez.AI.FSM;

public class Flying_1State : State<Player>
{
    public override void Update(float deltaTime)
    {
        _context.PhysicalUpdate();
    }
    public void slide()
    {
        _context.fsm.ChangeState<Sliding_1State>();
    }
}
