using System;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

class SpriteOverlay
{
    private Entity entity;
    private SpriteRenderer renderer;
    private bool alwaysFollow;
    private Func<bool> enableWhen;
    private Func<bool> disableWhen;

    public SpriteOverlay(Entity entity, string spriteName, Func<bool> enableWhen, Func<bool> disableWhen, bool alwaysFollow = false)
    {
        this.entity = entity;
        this.enableWhen = enableWhen;
        this.disableWhen = disableWhen;
        this.alwaysFollow = alwaysFollow;
        renderer = new SpriteRenderer(Game.Atlas.GetSprite(spriteName));
        entity.AddComponent(renderer);
    }

    public void Update()
    {
        if (!renderer.Enabled && enableWhen())
        {
            renderer.Enabled = true;
            renderer.Transform.Scale = (1f / entity.Scene.Camera.RawZoom) * Vector2.One;
        }
        else if (renderer.Enabled && disableWhen())
            renderer.Enabled = false;

        if (alwaysFollow && renderer.Enabled)
            renderer.Transform.Position = entity.Scene.Camera.Position;
    }
}

public class Overlay : Component, IUpdatable
{
    SpriteOverlay[] overlays;

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        overlays = new[] {
            new SpriteOverlay(Entity, "waiting_overlay", () => Game.State == GameState.Waiting, () => Game.State != GameState.Waiting),
            new SpriteOverlay(Entity, "pause_overlay", () => Game.IsPaused, () => !Game.IsPaused, true),
            new SpriteOverlay(Entity, "gameover_overlay", () => Game.State == GameState.Over, () => Game.State != GameState.Over, true)
        };
    }

    public void Update()
    {
        foreach (var overlay in overlays)
            overlay.Update();
    }
}