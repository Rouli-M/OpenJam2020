using System;
using System.Collections.Generic;
using System.Text;
using Nez.AI.FSM;

public class Sliding_1State : State<Player>
{
    public override void Begin()
    {
        base.Begin();
        _context.animator.Play("1-slide");
    }
    public override void Update(float deltaTime)
    {
        _context.PhysicalUpdate();
    }

}
