using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.UI;
using Nez.Tweens;
using Nez.ImGuiTools;
using Console = Nez.Console;
using Microsoft.Xna.Framework.Input;
using Nez.BitmapFonts;

public abstract class Scene : Nez.Scene, IFinalRenderDelegate
{
    public const int ScreenSpaceRenderLayer = 999;
    public UICanvas Canvas;

    Table _table;
    List<Button> _sceneButtons = new List<Button>();
    ScreenSpaceRenderer _screenSpaceRenderer;

    static ImGuiManager _imGuiManager;

    NezSpriteFont font;

    public Scene(bool addExcludeRenderer = true, bool needsFullRenderSizeForUi = false)
    {
        // Don't actually add the renderer since we will manually call it later
        _screenSpaceRenderer = new ScreenSpaceRenderer(100, ScreenSpaceRenderLayer);
        _screenSpaceRenderer.ShouldDebugRender = false;

        font = new NezSpriteFont(Content.Load<SpriteFont>("Font"));

        SetDesignResolution(Constants.DESIGN_WIDTH, Constants.DESIGN_HEIGHT, SceneResolutionPolicy.BestFit);
        Screen.SetSize(Constants.DESIGN_WIDTH, Constants.DESIGN_HEIGHT);

        FinalRenderDelegate = this;

        if (addExcludeRenderer)
            AddRenderer(new RenderLayerExcludeRenderer(0, ScreenSpaceRenderLayer));

        // Create our canvas and put it on the screen space render layer
        Canvas = CreateEntity("ui").AddComponent(new UICanvas());
        Canvas.IsFullScreen = true;
        Canvas.RenderLayer = ScreenSpaceRenderLayer;

        SetupSceneSelector();
    }

    IEnumerable<Type> GetTypesWithSampleSceneAttribute()
    {
        var assembly = typeof(Scene).Assembly;
        var scenes = assembly.GetTypes()
            .Where(t => t.GetCustomAttributes(typeof(SceneAttribute), true).Length > 0)
            .OrderBy(t =>
                ((SceneAttribute)t.GetCustomAttributes(typeof(SceneAttribute), true)[0]).Order);

        foreach (var s in scenes)
            yield return s;
    }

    void SetupSceneSelector()
    {
        _table = Canvas.Stage.AddElement(new Table());
        _table.SetFillParent(true).Right().Top();

        var topButtonStyle = new TextButtonStyle(new PrimitiveDrawable(Color.Black, 10f),
            new PrimitiveDrawable(Color.Yellow), new PrimitiveDrawable(Color.DarkSlateBlue))
        {
            DownFontColor = Color.Black
        };

        _table.Add(new TextButton("Toggle Scene List", topButtonStyle)).SetFillX().SetMinHeight(30)
            .GetElement<Button>().OnClicked += OnToggleSceneListClicked;

        _table.Row().SetPadTop(10);

        var checkbox = _table.Add(new CheckBox("Debug Render", new CheckBoxStyle
        {
            CheckboxOn = new PrimitiveDrawable(30, Color.Green),
            CheckboxOff = new PrimitiveDrawable(30, new Color(0x00, 0x3c, 0xe7, 0xff))
        })).GetElement<CheckBox>();

        checkbox.OnChanged += enabled => Core.DebugRenderEnabled = enabled;
        checkbox.IsChecked = Core.DebugRenderEnabled;
        _table.Row().SetPadTop(30);

        var buttonStyle = new TextButtonStyle(new PrimitiveDrawable(new Color(78, 91, 98), 10f),
            new PrimitiveDrawable(new Color(244, 23, 135)), new PrimitiveDrawable(new Color(168, 207, 115)))
        {
            DownFontColor = Color.Black
        };

        // Find every Scene with the SampleSceneAttribute and create a button for each one
        foreach (var type in GetTypesWithSampleSceneAttribute())
        {
            foreach (var attr in type.GetCustomAttributes(true))
            {
                if (attr.GetType() == typeof(SceneAttribute))
                {
                    var sampleAttr = attr as SceneAttribute;
                    var button = _table.Add(new TextButton(sampleAttr.ButtonName, buttonStyle)).SetFillX()
                        .SetMinHeight(30).GetElement<TextButton>();

                    _sceneButtons.Add(button);
                    button.OnClicked += butt =>
                    {
                        // Stop all tweens in case any demo scene started some up
                        TweenManager.StopAllTweens();
                        Core.GetGlobalManager<ImGuiManager>()?.SetEnabled(false);
                        Core.StartSceneTransition(new FadeTransition(() => Activator.CreateInstance(type) as Nez.Scene));
                    };

                    _table.Row().SetPadTop(10);
                }
            }
        }
    }

    void OnToggleSceneListClicked(Button butt)
    {
        foreach (var button in _sceneButtons)
            button.SetIsVisible(!button.IsVisible());
    }

    public override void Update()
    {
        if (Input.GamePads.Length > 0 && Input.GamePads[0].IsButtonPressed(Buttons.Start))
            ToggleImGui();

        if (Input.IsKeyPressed(Keys.Tab))
            ToggleImGui();

        if (Input.IsKeyPressed(Keys.P))
            Time.TimeScale = Time.TimeScale == 1 ? 0 : 1;

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

    #region IFinalRenderDelegate

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

        if (Time.TimeScale == 0)
        {
            Graphics.Instance.Batcher.Begin();
            Graphics.Instance.Batcher.DrawString(font, "Pause", new Vector2(550, 300), Color.Red);
            Graphics.Instance.Batcher.End();
        }

        _screenSpaceRenderer.Render(_scene);
    }

    #endregion
}

[AttributeUsage(AttributeTargets.Class)]
public class SceneAttribute : Attribute
{
    public string ButtonName;
    public int Order;

    public SceneAttribute(string buttonName, int order)
    {
        ButtonName = buttonName;
        Order = order;
    }
}