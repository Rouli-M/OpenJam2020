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
        Time.TimeScale = 0.1f;
    }

    public override void Update(float deltaTime)
    {
        _context.PhysicalUpdate();
        if (!_context.IsThrowInputGiven())
        {
            _context.Throw(800f);
            _context.fsm.ChangeState<Flying_2State>();
            _context.animator.Play("2-fly");
            _context.Entity.RemoveComponent<BoxCollider>();
            var collider = _context.Entity.AddComponent(new BoxCollider(_context.Box2.X, _context.Box2.Y));
            collider.ShouldColliderScaleAndRotateWithTransform = false;
            _context.Entity.Scene.CreateEntity("grand_dino").AddComponent(new DroppedDino(3));
        }
    }

    public override void End()
    {
        Time.TimeScale = 1;
    }
}
