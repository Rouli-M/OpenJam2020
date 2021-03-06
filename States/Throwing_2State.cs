﻿using Nez;
using Nez.AI.FSM;
using OpenJam2020.Components;

public class Throwing_2State : State<Player>
{
    public override void Begin()
    {
        _context.animator.Play("2-charge_throw");
        _context.hold_sound.Play();
        Time.TimeScale = 0.1f;
    }

    public override void Update(float deltaTime)
    {
        _context.PhysicalUpdate();
        if (!_context.IsThrowInputGiven())
        {
            _context.Throw(800f);
            _context.fsm.ChangeState<Flying_1State>();
            _context.animator.Play("1-fly");
            _context.Entity.GetComponent<BoxCollider>().SetSize(_context.Box1.X, _context.Box1.Y);
            _context.Entity.Scene.CreateEntity("petit_dino", _context.Transform.Position).AddComponent(new DroppedDino(2));
        }
    }

    public override void End()
    {
        Time.TimeScale = 1;
    }
}
