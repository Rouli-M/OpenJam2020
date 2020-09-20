using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;

class Decor : WorldObject
{
    string sprite_name;
    protected SpriteAnimator animator;

    public Decor(string sprite_name)
    {
        this.sprite_name = sprite_name;
    }

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        animator = Entity.AddComponent(new SpriteAnimator { LayerDepth = .6f });
        animator.AddAnimation("idle", new[] { Game.Atlas.GetSprite(sprite_name) });
        animator.OriginNormalized = new Vector2(0.5f, 1);
    }

}

