﻿using System;
using System.Collections.Generic;
using System.Text;
using OpenJam2020;
using Nez;
using Microsoft.Xna.Framework;

class WorldGenerator : Component
{
    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();
        generate();
    }

    public void generate()
    {
        Entity.Scene.CreateEntity("aa").AddComponent<Bumper>().Transform.Position = new Vector2(500, 500);

    }
}
