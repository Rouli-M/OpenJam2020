using Microsoft.Xna.Framework;
using Nez;

public class CameraFollow : Component, IUpdatable
{
    Player player;
    Vector2 offset;
    Landscape landscape;
    Camera camera;

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        player = Entity.Scene.FindComponentOfType<Player>();
        landscape = Entity.Scene.FindComponentOfType<Landscape>();
        camera = Entity.Scene.Camera;
    }

    public void Update()
    {
        offset = new Vector2(0, .2f * Constants.DESIGN_HEIGHT) + new Vector2(0, -150) - 0f * player.Velocity;
        Vector2 newPosition = player.Transform.Position - offset;

        float length = player.Velocity.Length();
        float normalized = Mathf.Clamp01(length / 3000f);
        camera.RawZoom = Mathf.Lerp(0.8f, .3f, normalized);

        Transform.Position = newPosition * 0.1f + 0.9f * Transform.Position;

        landscape.Scroll(player.Velocity * Time.DeltaTime);
    }
}