using Nez;

public class DefaultScene : Scene
{
    Player player;

    public override void Initialize()
    {
        base.Initialize();

        player = CreateEntity("player").AddComponent<Player>();

        CreateEntity("ground").AddComponent<Ground>();
        CreateEntity("background").AddComponent<Landscape>();
        CreateEntity("world_generator").AddComponent<WorldGenerator>();

        Camera.Entity.AddComponent<CameraFollow>();
        Camera.Entity.AddComponent<CameraShake>();
    }

    public override void Update()
    {
        base.Update();
        Game.Score = (int)(player.Transform.Position.X / 50);
    }
}