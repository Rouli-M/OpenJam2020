using System;
using System.Collections.Generic;
using System.Text;
using Nez;
using Nez.AI.FSM;
using OpenJam2020.Components;

public class Throwing_2State : State<Player>
{
    public override void Update(float deltaTime)
    {
        _context.PhysicalUpdate(0.1f);
        if (!_context.IsThrowInputGiven())
        {
            _context.Throw(600f);
            _context.fsm.ChangeState<Flying_1State>();
            _context.animator.Play("1-fly");
            _context.Entity.RemoveComponent<BoxCollider>();
            _context.Entity.AddComponent(new BoxCollider(_context.Box1.X, _context.Box1.Y));
            _context.Entity.Scene.CreateEntity("petit_dino").AddComponent(new DroppedDino(2));
        }
    }
}
