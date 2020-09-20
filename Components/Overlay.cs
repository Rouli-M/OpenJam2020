using System;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

class SpriteOverlay
{
    private Entity entity;
    private SpriteRenderer renderer;
    private Func<bool> enableWhen;
    private Func<bool> disableWhen;

    public SpriteOverlay(Entity entity, string spriteName, Func<bool> enableWhen, Func<bool> disableWhen)
    {
        this.entity = entity;
        this.enableWhen = enableWhen;
        this.disableWhen = disableWhen;

        renderer = entity.AddComponent(new SpriteRenderer(Game.Atlas.GetSprite(spriteName)));
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

        if (renderer.Enabled)
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
            new SpriteOverlay(Entity, "pause_overlay", () => Game.IsPaused, () => !Game.IsPaused),
            new SpriteOverlay(Entity, "gameover_overlay", () => Game.State == GameState.Over, () => Game.State != GameState.Over)
        };
    }

    public void Update()
    {
        foreach (var overlay in overlays)
            overlay.Update();
    }
}