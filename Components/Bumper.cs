using Microsoft.Xna.Framework;

class Bumper : WorldObject
{
    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();
    }

    public override Vector2 Collision(Player player, Vector2 Normal)
    {
        return player.Velocity - 2 * Normal * Vector2.Dot(player.Velocity, Normal);
    }
}
