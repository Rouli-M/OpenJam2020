using Nez;
using Nez.Sprites;

class Pterodactylus : WorldObject, ITriggerListener
{
    protected SpriteAnimator animator;

    public override void OnAddedToEntity()
    {
        var collider = Entity.AddComponent(new BoxCollider(-75, -75, 250, 150));
        collider.IsTrigger = true;

        animator = Entity.AddComponent(new SpriteAnimator() { LayerDepth = .5f });
        animator.AddAnimation("idle", Game.Atlas.GetAnimation("ptero"));
        animator.Play("idle");

        base.OnAddedToEntity();
    }

    public void OnTriggerEnter(Collider other, Collider local)
    {
        var player = other.GetComponent<Player>();
        if (player.Velocity.X < 2000)
            player.Velocity.X += 300;
    }

    public void OnTriggerExit(Collider other, Collider local)
    {
    }
}
