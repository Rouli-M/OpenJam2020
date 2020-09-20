using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

class TallBumper : Bumper
{
    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        Entity.GetComponent<BoxCollider>().SetLocalOffset(new Vector2(0, -491));
        Entity.GetComponent<BoxCollider>().SetSize(220, 40);

        Entity.RemoveComponent<SpriteAnimator>();
        animator = Entity.AddComponent(new SpriteAnimator() { LayerDepth = .5f });
        animator.AddAnimation("idle", new[] { Game.Atlas.GetSprite("tall_champi1") });
        animator.AddAnimation("bump", Game.Atlas.GetAnimation("tall_champi"));
    }
}