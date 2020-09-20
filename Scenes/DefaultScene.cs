using Nez;

public class DefaultScene : Scene
{
    public override void Initialize()
    {
        base.Initialize();

        CreateEntity("ground").AddComponent<Ground>();
        CreateEntity("player").AddComponent<Player>();
        CreateEntity("background").AddComponent<Landscape>();
        CreateEntity("world_generator").AddComponent<WorldGenerator>();

        Camera.Entity.AddComponent<CameraFollow>();
        Camera.Entity.AddComponent<CameraShake>();
    }
}