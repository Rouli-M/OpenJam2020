using System;
using System.Collections.Generic;
using System.Text;
using Nez.AI.FSM;

public class Flying_3State : State<Player>
{
    public override void Update(float deltaTime)
    {
        _context.PhysicalUpdate();
        if (_context.Velocity.Y > 0 && _context.animator.CurrentAnimationName != "3-fall")
            _context.animator.Play("3-fall");
        if (_context.Velocity.Y < 0 && _context.animator.CurrentAnimationName != "3-rise")
            _context.animator.Play("3-rise");

        if (_context.IsThrowInputGiven())
        {
            _context.fsm.ChangeState<Throwing_3State>();
            _context.animator.Play("3-charge_throw");
        }
    }
    public void slide()
    {
        _context.fsm.ChangeState<Sliding_3State>();
        _context.animator.Play("3-slide");
    }
}
