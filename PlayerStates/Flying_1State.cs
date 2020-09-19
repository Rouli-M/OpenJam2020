using System;
using System.Collections.Generic;
using System.Text;
using Nez.AI.FSM;

public class Flying_1State : State<Player>
{
    public override void Begin()
    {
        base.Begin();
        _context.animator.Play("1-fly");
    }
    public override void Update(float deltaTime)
    {
        _context.PhysicalUpdate();
    }
}
