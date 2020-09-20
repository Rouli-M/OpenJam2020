using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using System;

class Booster : Pterodactylus
{
    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();
        Entity.GetComponent<BoxCollider>().SetLocalOffset(new Vector2(0, 0));
        Entity.GetComponent<BoxCollider>().SetSize(300, 100);

        Entity.RemoveComponent<SpriteAnimator>();
        animator = Entity.AddComponent(new SpriteAnimator() { LayerDepth = .5f });
        animator.AddAnimation("idle", Game.Atlas.GetAnimation("boost"));
        animator.Play("idle");
    }

    public new void Update()
    {
        if (isTriggeredByPlayer)
        {
            /*
            if (Game.player.Velocity.X < 80000) Game.player.Velocity.X = 80000;
            Game.player.Velocity.X += 100000 * Time.DeltaTime * Constants.speedMultiplier;
            */
        }
    }
}
