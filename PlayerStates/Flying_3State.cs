using System;
using System.Collections.Generic;
using System.Text;
using Nez.AI.FSM;

public class Flying_3State : State<Player>
{
    public override void Begin()
    {
        base.Begin();
        _context.animator.Play("3-rise");
    }

    public override void Update(float deltaTime)
    {
        _context.PhysicalUpdate();
        if (_context.Velocity.Y > 30 && _context.animator.CurrentAnimationName != "3-fall")
            _context.animator.Play("3-fall");
        if (_context.Velocity.Y < 30 && _context.animator.CurrentAnimationName != "3-rise")
            _context.animator.Play("3-rise");
        if ((Math.Abs(_context.Velocity.Y) <= 30) &&  _context.animator.CurrentAnimationName != "3-top")
            _context.animator.Play("3-top");

        if (_context.IsThrowInputGiven())
            _context.fsm.ChangeState<Throwing_3State>();
    }
    public void slide()
    {
        _context.fsm.ChangeState<Sliding_3State>();
    }
}
