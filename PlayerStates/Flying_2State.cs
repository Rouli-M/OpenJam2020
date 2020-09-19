using System;
using System.Collections.Generic;
using System.Text;
using Nez.AI.FSM;

public class Flying_2State : State<Player>
{
    public override void Update(float deltaTime)
    {
        _context.PhysicalUpdate();

        if (_context.isThrowInputGiven())
        {
            _context.fsm.ChangeState<Throwing_2State>();
            _context.animator.Play("2-charge_throw");
        }
    }
}
