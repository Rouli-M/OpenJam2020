using System;
using System.Collections.Generic;
using System.Text;
using Nez.AI.FSM;

public class Throwing_3State : State<Player>
{
    public override void Update(float deltaTime)
    {
        if (!_context.isThrowInputGiven())
        {
            _context.Throw(10f);
            _context.fsm.ChangeState<Flying_2State>();
            _context.animator.Play("2-fly");
        }
    }
}
