using Microsoft.Xna.Framework;
using Nez;

public class Camera : Component, IUpdatable
{
    Player player;
    Vector2 offset;

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();
        player = Entity.Scene.FindComponentOfType<Player>();
        offset = new Vector2(0, .2f * Constants.DESIGN_HEIGHT);
    }

    public void Update()
    {
        Transform.Position = player.Transform.Position - offset;
    }
}