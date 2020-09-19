using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Nez.AI.FSM;

public class NotThrownState : State<Player>
{
    public override void Update(float deltaTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Space))
        {
            _context.Throw((float)Math.PI / 4, 10f);
            _context.fsm.ChangeState<Flying_3State>();
        }
    }
}
