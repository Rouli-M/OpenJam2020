using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using System;

class Balloon : WorldObject, ITriggerListener
{
    protected const float radius = 150;
    protected const float minXVelocity = 100;

    protected SpriteAnimator animator;
    protected SoundEffect bounceSound;

    public override void OnAddedToEntity()
    {
        var collider = Entity.AddComponent(new CircleCollider(radius));
        collider.IsTrigger = true;

        animator = Entity.AddComponent(new SpriteAnimator() { LayerDepth = .5f });
        animator.AddAnimation("idle", new[] { Game.Atlas.GetSprite("montgolfiere") });

        bounceSound = Core.Content.Load<SoundEffect>("bounce");

        base.OnAddedToEntity();
    }

    public void OnTriggerEnter(Collider other, Collider local)
    {
        Vector2 distance = Transform.Position - other.Transform.Position;
        var player = other.GetComponent<Player>();

        Vector2 bounceForce = -2 * Vector2.Dot(player.Velocity, Vector2.Normalize(distance)) * Vector2.Normalize(distance);

        player.Velocity += bounceForce;
        if (player.Velocity.X < minXVelocity)
        {
            player.Velocity.X = (float)Math.Max(-player.Velocity.X, minXVelocity);
        }

        bounceSound.Play();
    }

    public void OnTriggerExit(Collider other, Collider local)
    {
    }
}
