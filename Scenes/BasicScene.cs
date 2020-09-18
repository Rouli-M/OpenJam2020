using Nez;

[Scene("Basic Scene", 0)]
public class BasicScene : Scene
{
    public override void Initialize()
    {
        base.Initialize();

        SetDesignResolution(512, 256, SceneResolutionPolicy.FixedHeightPixelPerfect);
        Screen.SetSize(512 * 3, 256 * 3);
    }
}