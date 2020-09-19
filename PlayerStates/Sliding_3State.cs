using System;
using System.Collections.Generic;
using System.Text;
using Nez.AI.FSM;

public class Sliding_3State : State<Player>
{
    public override void Update(float deltaTime)
    {
        _context.PhysicalUpdate();

        if (_context.IsThrowInputGiven())
        {
            _context.fsm.ChangeState<Throwing_3State>();
            _context.animator.Play("3-charge_throw");
        }
    }
}
