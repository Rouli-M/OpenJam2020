﻿using System;
using System.Collections.Generic;
using System.Text;
using Nez.AI.FSM;

public class Sliding_1State : State<Player>
{
    public override void Update(float deltaTime)
    {
        _context.PhysicalUpdate();
    }
}