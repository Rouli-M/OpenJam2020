using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Console;
using Nez.Sprites;

public class Game : Core
{
    public static bool IsPaused;
    public static SpriteAtlas Atlas;

    protected override void Initialize()
    {
        base.Initialize();
        Window.Title = "Game";
        Window.AllowUserResizing = true;
        DebugConsole.ConsoleKey = Keys.F3;
        Screen.SynchronizeWithVerticalRetrace = true;
        DefaultSamplerState = new SamplerState { Filter = TextureFilter.Linear };
        Scene = new BasicScene();
        Atlas = Content.LoadSpriteAtlas("Content/bundle.atlas");
    
        TargetElapsedTime = TimeSpan.FromTicks(10000000 / 60);
        IsFixedTimeStep = true;
    }
    protected override void LoadContent()
    {
        Content.Load<SoundEffect>("charge_up");
        Content.Load<SoundEffect>("success");
        Content.Load<SoundEffect>("throw1");
        Content.Load<SoundEffect>("bounce");
        Content.Load<SoundEffect>("hold");
        base.LoadContent();
    }
}