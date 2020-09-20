using Microsoft.Xna.Framework.Audio;
using Nez;
using Nez.Sprites;

class Tower : WorldObject, ITriggerListener
{
    protected SpriteAnimator animator;
    protected SoundEffect breakSound;

    public override void OnAddedToEntity()
    {
        var collider = Entity.AddComponent(new BoxCollider(-100, -630, 200, 1370));
        collider.IsTrigger = true;

        animator = Entity.AddComponent(new SpriteAnimator() { LayerDepth = .5f });
        animator.AddAnimation("idle", new[] { Game.Atlas.GetSprite("future_tower") });
        animator.AddAnimation("break", new[] { Game.Atlas.GetSprite("bg3") });

        breakSound = Core.Content.Load<SoundEffect>("stomp");

        base.OnAddedToEntity();
    }

    public void OnTriggerEnter(Collider other, Collider local)
    {
        var player = other.GetComponent<Player>();
        player.Velocity.X *= 0.7f;

        animator.Play("break", SpriteAnimator.LoopMode.Once);

        breakSound.Play();
    }

    public void OnTriggerExit(Collider other, Collider local)
    {
    }
}
