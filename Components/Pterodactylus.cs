using Nez;
using Nez.Sprites;

class Pterodactylus : WorldObject, ITriggerListener, IUpdatable
{
    protected SpriteAnimator animator;
    protected bool isTriggeredByPlayer;

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
            player.Velocity.Y = 0;
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
        if (isTriggeredByPlayer && Game.player.Velocity.X < 2000)
        {
            Game.player.Velocity.X += 300 * Time.DeltaTime * Constants.speedMultiplier;
             
        }
    }
}
