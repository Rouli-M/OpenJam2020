﻿using System;
using Nez.AI.FSM;

public class Flying_3State : State<Player>
{
    public override void Begin()
    {
        base.Begin();
        _context.animator.Play("3-rise");
        _context.flyingParticles.Play();
    }

    public override void Update(float deltaTime)
    {
        if (_context.Velocity.Y > 30 && _context.animator.CurrentAnimationName != "3-fall")
            _context.animator.Play("3-fall");
        if (_context.Velocity.Y < - 30 && _context.animator.CurrentAnimationName != "3-rise")
            _context.animator.Play("3-rise");
        if ((Math.Abs(_context.Velocity.Y) <= 30) && _context.animator.CurrentAnimationName != "3-top")
            _context.animator.Play("3-top");

        if (_context.IsThrowInputGiven())
            _context.fsm.ChangeState<Throwing_3State>();

        _context.PhysicalUpdate();
    }
}
