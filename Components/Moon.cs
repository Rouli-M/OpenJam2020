using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using System;

class Moon : Balloon
{
    protected const float moonRadius = 300;

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        Entity.GetComponent<CircleCollider>().SetRadius(moonRadius);

        Entity.RemoveComponent<SpriteAnimator>();
        animator = Entity.AddComponent(new SpriteAnimator() { LayerDepth = .5f });
        animator.AddAnimation("idle", new[] { Game.Atlas.GetSprite("lune") });

        var collider = Entity.AddComponent(new CircleCollider(radius));
        collider.IsTrigger = true;

        animator = Entity.AddComponent(new SpriteAnimator() { LayerDepth = .5f });
        animator.AddAnimation("idle", new[] { Game.Atlas.GetSprite("lune") });

        bounceSound = Core.Content.Load<SoundEffect>("bounce");
    }
}
