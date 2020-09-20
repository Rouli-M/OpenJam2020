using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.ImGuiTools;
using Console = Nez.Console;
using Microsoft.Xna.Framework.Input;

public abstract class Scene : Nez.Scene, IFinalRenderDelegate
{
    public const int ScreenSpaceRenderLayer = 999;
    public UICanvas Canvas;

    ScreenSpaceRenderer _screenSpaceRenderer;
    static ImGuiManager _imGuiManager;

    public Scene(bool addExcludeRenderer = true, bool needsFullRenderSizeForUi = false)
    {
        // Don't actually add the renderer since we will manually call it later
        _screenSpaceRenderer = new ScreenSpaceRenderer(100, ScreenSpaceRenderLayer);
        _screenSpaceRenderer.ShouldDebugRender = false;

        SetDesignResolution(Constants.DESIGN_WIDTH, Constants.DESIGN_HEIGHT, SceneResolutionPolicy.BestFit);
        Screen.SetSize(Constants.DESIGN_WIDTH, Constants.DESIGN_HEIGHT);

        FinalRenderDelegate = this;

        if (addExcludeRenderer)
            AddRenderer(new RenderLayerExcludeRenderer(0, ScreenSpaceRenderLayer));

        var ui = CreateEntity("ui").AddComponent<Overlay>();

        Game.PauseOnFocusLost = false;
    }

    public override void Update()
    {
        if (Input.IsKeyPressed(Keys.Tab))
            ToggleImGui();

        if (!Game.Instance.IsActive)
            Game.IsPaused = true;

        if (Input.IsKeyPressed(Keys.P))
        {
            Game.IsPaused = !Game.IsPaused;
            Time.TimeScale = Game.IsPaused ? 0 : 1;
        }
        else if (Game.IsPaused)
            Time.TimeScale = 0;

        base.Update();
    }

    [Console.Command("toggle-imgui", "Toggles the Dear ImGui renderer")]
    static void ToggleImGui()
    {
        _imGuiManager = Core.GetGlobalManager<ImGuiManager>();

        if (_imGuiManager == null)
        {
            _imGuiManager = new ImGuiManager { ShowSeperateGameWindow = false };
            Core.RegisterGlobalManager(_imGuiManager);
        }
        else
        {
            _imGuiManager.Enabled = !_imGuiManager.Enabled;
        }
    }

    private Nez.Scene _scene;

    public void OnAddedToScene(Nez.Scene scene) => _scene = scene;

    public void OnSceneBackBufferSizeChanged(int newWidth, int newHeight) => _screenSpaceRenderer.OnSceneBackBufferSizeChanged(newWidth, newHeight);

    public void HandleFinalRender(RenderTarget2D finalRenderTarget, Color letterboxColor, RenderTarget2D source,
                                  Rectangle finalRenderDestinationRect, SamplerState samplerState)
    {
        Core.GraphicsDevice.SetRenderTarget(null);
        Core.GraphicsDevice.Clear(letterboxColor);

        Graphics.Instance.Batcher.Begin(BlendState.Opaque, samplerState, DepthStencilState.None, RasterizerState.CullNone, null);
        Graphics.Instance.Batcher.Draw(source, finalRenderDestinationRect, Color.White);
        Graphics.Instance.Batcher.End();

        _screenSpaceRenderer.Render(_scene);
    }
}