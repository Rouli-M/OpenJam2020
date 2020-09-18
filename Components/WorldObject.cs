using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Nez;
using OpenJam2020;

abstract class WorldObject : Component
{
    public abstract Vector2 Collision(Player player, Vector2 Normal);

}
