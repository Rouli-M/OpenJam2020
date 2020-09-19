using System;
using System.Collections.Generic;
using System.Text;
using Nez.AI.FSM;
using OpenJam2020.Components;

public class ThrowingState : State<Player>
{
    public override void Update(float deltaTime)
    {
        if (!_context.IsThrowInputGiven())
        {
            var entity = _context.Entity.Scene.CreateEntity("canon_empty", _context.Transform.Position).AddComponent(new DroppedDino(0));
            entity.Transform.Rotation = _context.Entity.Rotation;
            _context.Throw(1200f, -_context.Entity.Rotation);
            _context.fsm.ChangeState<Flying_3State>();
            _context.animator.Play("3-rise");
        }
    }
}
