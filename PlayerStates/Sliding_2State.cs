using System;
using System.Collections.Generic;
using System.Text;
using Nez.AI.FSM;

public class Sliding_2State : State<Player>
{
    public override void Begin()
    {
        base.Begin();
        _context.animator.Play("2-slide");
    }
    public override void Update(float deltaTime)
    {
        _context.PhysicalUpdate();

        if (_context.IsThrowInputGiven())
            _context.fsm.ChangeState<Throwing_2State>();
    }

    internal void fly()
    {
        throw new NotImplementedException();
    }
}
