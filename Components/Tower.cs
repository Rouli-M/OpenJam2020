using Microsoft.Xna.Framework.Audio;
using Nez;
using Nez.Sprites;

class Tower : WorldObject, ITriggerListener
{
    protected SpriteAnimator animator;
    protected SoundEffect breakSound;

    public override void OnAddedToEntity()
    {
        var collider = Entity.AddComponent(new BoxCollider(100, 150));
        collider.IsTrigger = true;

        animator = Entity.AddComponent(new SpriteAnimator() { LayerDepth = .5f });
        animator.AddAnimation("idle", new[] { Game.Atlas.GetSprite("tower1") });
        animator.AddAnimation("break", Game.Atlas.GetAnimation("tower"));

        breakSound = Core.Content.Load<SoundEffect>("tower_break");

        base.OnAddedToEntity();
    }

    public void OnTriggerEnter(Collider other, Collider local)
    {
        var player = other.GetComponent<Player>();
        player.Velocity.X /= 2;

        animator.Play("break", SpriteAnimator.LoopMode.Once);
    }

    public void OnTriggerExit(Collider other, Collider local)
    {
    }
}
