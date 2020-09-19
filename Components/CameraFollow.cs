using Microsoft.Xna.Framework;
using Nez;

public class CameraFollow : Component, IUpdatable
{
    Player player;
    Vector2 offset;
    Landscape landscape;

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();
        
        player = Entity.Scene.FindComponentOfType<Player>();
        landscape = Entity.Scene.FindComponentOfType<Landscape>();
    }

    float maxSpeed = 100;

    public void Update()
    {
        offset = new Vector2(0, .2f * Constants.DESIGN_HEIGHT) + new Vector2(0, -150) - 0.3f *  player.Velocity;
        var newPosition = player.Transform.Position - offset;
        var delta = newPosition - Transform.Position;
        var length = delta.Length();

        var camera = Entity.Scene.Camera;
        var normalized = Mathf.Clamp01(length / maxSpeed);

        camera.RawZoom = Mathf.Lerp(0.8f, .3f, normalized);

        Transform.Position = newPosition;

        landscape.Scroll(delta);
    }
}