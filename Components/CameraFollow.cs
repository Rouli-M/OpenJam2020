using Microsoft.Xna.Framework;
using Nez;

public class CameraFollow : Component, IUpdatable
{
    Player player;
    Vector2 offset, previous_offset;
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

        offset = player.Velocity;
        if (offset.Length() > 5) offset.Normalize(); 
        else offset = Vector2.Zero;
        offset *= -300; offset.X *= 2; if (player.Velocity.Length() < 1000) offset.X *= (player.Velocity.Length())/1000;
        Vector2 newPosition = player.Transform.Position - offset * 0.5f - previous_offset * 0.5f;

        float length = player.Velocity.Length();
        float normalized = Mathf.Clamp01(length / 3000f);
        camera.RawZoom = Mathf.Lerp(0.8f, .3f, normalized);

        Transform.Position = newPosition * 0.1f + 0.9f * Transform.Position;

        landscape.Scroll(player.Velocity * Time.DeltaTime);

        previous_offset = offset;
    }
}