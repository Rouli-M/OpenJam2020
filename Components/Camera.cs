using Microsoft.Xna.Framework;
using Nez;

public class Camera : Component, IUpdatable
{
    Player player;
    Vector2 offset;
    Landscape landscape;

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();
        offset = new Vector2(0, .2f * Constants.DESIGN_HEIGHT);
        player = Entity.Scene.FindComponentOfType<Player>();
        landscape = Entity.Scene.FindComponentOfType<Landscape>();
    }

    public void Update()
    {
        var newPosition = player.Transform.Position - offset;
        var delta = newPosition - Transform.Position;

        Transform.Position = newPosition;

        landscape.Scroll(delta);
    }
}