using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Console;

namespace OpenJam2020
{
    public class Game : Core
    {
        protected override void Initialize()
        {
            base.Initialize();
            Window.Title = "Game";
            Window.AllowUserResizing = true;
            DebugConsole.ConsoleKey = Keys.F3;
            Screen.SynchronizeWithVerticalRetrace = true;
            Core.DefaultSamplerState = new SamplerState { Filter = TextureFilter.Linear };
            Scene = new BasicScene();
        }
    }
}