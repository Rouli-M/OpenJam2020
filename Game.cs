using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Console;
using Nez.ImGuiTools;
using Nez.Sprites;
using Nez.Tweens;

public enum GameState
{
    Waiting,
    Playing,
    Over
}

public class Game : Core
{
    public static bool IsPaused;
    public static SpriteAtlas Atlas;
    public static GameState State;
    public static int Score;
    public static Player player;

    protected override void Initialize()
    {
        base.Initialize();

        Window.Title = "Game";
        Window.AllowUserResizing = true;
        DebugConsole.ConsoleKey = Keys.F3;

        Screen.SynchronizeWithVerticalRetrace = true;
        DefaultSamplerState = new SamplerState { Filter = TextureFilter.Linear };
        TargetElapsedTime = TimeSpan.FromTicks(10000000 / 60);
        IsFixedTimeStep = true;
        Game.PauseOnFocusLost = false;

        Atlas = Content.LoadSpriteAtlas("Content/bundle.atlas");
        Scene = new DefaultScene();
    }

    protected override void LoadContent()
    {
        Content.Load<SoundEffect>("charge_up");
        Content.Load<SoundEffect>("success");
        Content.Load<SoundEffect>("throw1");
        Content.Load<SoundEffect>("bounce");
        Content.Load<SoundEffect>("hold");
        Content.Load<SoundEffect>("stomp");
        base.LoadContent();
    }

    public static void Restart()
    {
        // Stop all tweens in case any demo scene started some up
        TweenManager.StopAllTweens();
        Core.GetGlobalManager<ImGuiManager>()?.SetEnabled(false);
        Core.StartSceneTransition(new FadeTransition(() => Activator.CreateInstance(typeof(DefaultScene)) as Nez.Scene));
    }
}