using Microsoft.Xna.Framework.Audio;
using Nez;
using Nez.Sprites;

class Moon : Balloon
{
    protected const float moonRadius = 220;

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        Entity.GetComponent<CircleCollider>().SetRadius(moonRadius);

        Entity.RemoveComponent<SpriteAnimator>();
        animator = Entity.AddComponent(new SpriteAnimator() { LayerDepth = .5f });
        animator.AddAnimation("idle", new[] { Game.Atlas.GetSprite("lune") });
    }
}
