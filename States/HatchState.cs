﻿using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.AI.FSM;

public class HatchState : State<Player>
{
    public override void Begin()
    {
        base.Begin();
        Game.State = GameState.Over;

        _context.animator.Play("1-win");
        _context.success.Play();
        
        (_context.Entity.Scene as DefaultScene).PlayConfettis();
    }

    public override void Update(float deltaTime)
    {
        if (Input.IsKeyPressed(Keys.R))
            Game.Restart();
    }
}
