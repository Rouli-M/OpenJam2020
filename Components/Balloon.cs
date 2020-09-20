using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

class Balloon : WorldObject, ITriggerListener
{
    protected SpriteAnimator animator;
    protected SoundEffect bounceSound;

    public override void OnAddedToEntity()
    {
        var collider = Entity.AddComponent(new BoxCollider(-110, -10, 220, 80));
        collider.IsTrigger = true;

        animator = Entity.AddComponent(new SpriteAnimator() { LayerDepth = .5f });
        animator.AddAnimation("idle", new[] { Game.Atlas.GetSprite("mongolfiere") });

        bounceSound = Core.Content.Load<SoundEffect>("bounce");

        base.OnAddedToEntity();
    }
    
    public void OnTriggerEnter(Collider other, Collider local)
    {
        var player = other.GetComponent<Player>();
        //player.Velocity += ;

        bounceSound.Play();

        animator.Play("bump", SpriteAnimator.LoopMode.Once);
    }

    public void OnTriggerExit(Collider other, Collider local)
    {
    }
}
