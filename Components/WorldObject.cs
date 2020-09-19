using Microsoft.Xna.Framework;
using Nez;

abstract class WorldObject : Component
{
    public abstract Vector2 Collision(Player player, Vector2 Normal);

}
