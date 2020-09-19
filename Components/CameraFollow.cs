using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
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
       // Vector2 delta = player.Velocity;
        float length = player.Velocity.Length() * Time.DeltaTime;

        

        camera.RawZoom = Mathf.Lerp(0.8f, .3f, Mathf.Clamp01(length / 100f));

        // Transform.Position = player.Entity.Position;//
        Transform.Position =  newPosition * 0.1f + 0.9f * Transform.Position;

        landscape.Scroll(player.Velocity * Time.DeltaTime);
    }
}