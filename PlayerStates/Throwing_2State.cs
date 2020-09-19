using System;
using System.Collections.Generic;
using System.Text;
using Nez.AI.FSM;

public class Throwing_2State : State<Player>
{
    public override void Update(float deltaTime)
    {
        _context.PhysicalUpdate(0.1f);
        if (!_context.isThrowInputGiven())
        {
            _context.Throw(600f);
            _context.fsm.ChangeState<Flying_1State>();
            _context.animator.Play("1-fly");
        }
    }
}
