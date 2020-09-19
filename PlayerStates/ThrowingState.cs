using System;
using System.Collections.Generic;
using System.Text;
using Nez.AI.FSM;

public class ThrowingState : State<Player>
{
    public override void Update(float deltaTime)
    {
        if (!_context.IsThrowInputGiven())
        {
            _context.Throw(600f, (float)Math.PI / 4);
            _context.fsm.ChangeState<Flying_3State>();
            _context.animator.Play("3-rise");
        }
    }
}
