using System;
using System.Collections.Generic;
using System.Text;
using Nez;
using Nez.AI.FSM;
using OpenJam2020.Components;

public class Throwing_3State : State<Player>
{
    public override void Begin()
    {
        _context.animator.Play("3-charge_throw");
        _context.hold_sound.Play();
        Time.TimeScale = 0.1f;
    }

    public override void Update(float deltaTime)
    {
        _context.PhysicalUpdate();
        if (!_context.IsThrowInputGiven())
        {
            _context.Throw(800f);
            _context.fsm.ChangeState<Flying_2State>();
            _context.Entity.RemoveComponent<BoxCollider>();
            var collider = _context.Entity.AddComponent(new BoxCollider(_context.Box2.X, _context.Box2.Y));
            collider.ShouldColliderScaleAndRotateWithTransform = false;
            _context.Entity.Scene.CreateEntity("grand_dino", _context.Transform.Position).AddComponent(new DroppedDino(3));
        }
    }

    public override void End()
    {
        Time.TimeScale = 1;
    }
}
