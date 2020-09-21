using Nez;
using Nez.Sprites;

class Pterodactylus : WorldObject, ITriggerListener, IUpdatable
{
    protected SpriteAnimator animator;
    protected bool isTriggeredByPlayer;
    protected float boostForce = 300;

    public override void OnAddedToEntity()
    {
        var collider = Entity.AddComponent(new BoxCollider(-75, -75, 250, 150));
        collider.IsTrigger = true;

        isTriggeredByPlayer = false;

        animator = Entity.AddComponent(new SpriteAnimator() { LayerDepth = .5f });
        animator.AddAnimation("idle", Game.Atlas.GetAnimation("ptero"));
        animator.Play("idle");

        base.OnAddedToEntity();
    }

    public void OnTriggerEnter(Collider other, Collider local)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            player.Velocity.Y /= 2;
            isTriggeredByPlayer = true;
        }
    }

    public void OnTriggerExit(Collider other, Collider local)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
            isTriggeredByPlayer = false;
    }

    public void Update()
    {
        if (isTriggeredByPlayer && Game.player.Velocity.X < 1500)
        {
            Game.player.Velocity.X += boostForce * Time.DeltaTime * Constants.speedMultiplier;
        }
    }
}
