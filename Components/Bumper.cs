using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;

class Bumper : WorldObject, ITriggerListener
{
    private const float minBumpVelocity = 500f;
    private readonly Vector2 bumpDirection = new Vector2(0, -1);
    private SpriteAnimator animator;

    public override void OnAddedToEntity()
    {
        var collider = Entity.AddComponent(new BoxCollider(-110, -10, 220, 80));
        collider.IsTrigger = true;

        SpriteAtlas atlas = Entity.Scene.Content.LoadSpriteAtlas("Content/bundle.atlas");
        animator = new SpriteAnimator();
        animator.AddAnimation("idle", new[] { atlas.GetSprite("champi1") });
        animator.AddAnimation("bump", atlas.GetAnimation("champi"));
        Entity.AddComponent(animator);

        base.OnAddedToEntity();
    }

    public void OnTriggerEnter(Collider other, Collider local)
    {
        var player = other.GetComponent<Player>();
        player.Velocity += MathF.Max(2 * Vector2.Dot(-player.Velocity, bumpDirection), minBumpVelocity) * bumpDirection;
        animator.Play("bump", SpriteAnimator.LoopMode.Once);
    }

    public void OnTriggerExit(Collider other, Collider local)
    {

    }
}
