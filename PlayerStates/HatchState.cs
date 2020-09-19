using System;
using System.Collections.Generic;
using System.Text;
using Nez.AI.FSM;

public class HatchState : State<Player>
{
    public override void Begin()
    {
        base.Begin();
        Game.State = GameState.Over;
    }

    public override void Update(float deltaTime)
    {

    }
}
