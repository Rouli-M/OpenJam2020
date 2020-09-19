using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;

class TallBumper : Bumper
{
    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();
        Entity.GetComponent<BoxCollider>().SetLocalOffset(new Vector2(0, -491));
        Entity.GetComponent<BoxCollider>().SetSize(220, 40);

        Entity.RemoveComponent<SpriteAnimator>();
        animator = new SpriteAnimator();
        animator.AddAnimation("idle", new[] { Game.Atlas.GetSprite("tall_champi1") });
        animator.AddAnimation("bump", Game.Atlas.GetAnimation("tall_champi"));
        Entity.AddComponent(animator);

        bounce_sound = Core.Content.Load<SoundEffect>("bounce");

    }
}
