using System;
using System.Collections.Generic;
using System.Text;
using Nez.AI.FSM;

public class HatchState : State<Player>
{
    public override void Begin()
    {
        base.Begin();
        _context.animator.Play("1-win");
    }
    public override void Update(float deltaTime)
    {

    }
}
