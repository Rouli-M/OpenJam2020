using OpenJam2020;

[Scene("Basic Scene", 0)]
public class BasicScene : Scene
{
    public override void Initialize()
    {
        base.Initialize();
        CreateEntity("player").AddComponent<Player>();
    }
}