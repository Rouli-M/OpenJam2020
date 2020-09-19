using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Nez.AI.FSM;

public class NotThrownState : State<Player>
{
    public override void Update(float deltaTime)
    {
        if (_context.isThrowInputGiven())
        {
            _context.fsm.ChangeState<ThrowingState>();
        }
    }
}
