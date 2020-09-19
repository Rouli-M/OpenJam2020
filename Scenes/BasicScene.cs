[Scene("Basic Scene", 0)]
public class BasicScene : Scene
{
    public override void Initialize()
    {
        base.Initialize();

        CreateEntity("ground").AddComponent<Ground>();
        CreateEntity("player").AddComponent<Player>();
        CreateEntity("background").AddComponent<Landscape>();

        Camera.Entity.AddComponent<CameraFollow>();
    }
}