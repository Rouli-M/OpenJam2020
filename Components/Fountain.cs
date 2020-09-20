using Nez;
using Nez.Sprites;

class Fountain : Bumper
{
    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        Entity.GetComponent<BoxCollider>().SetSize(220, 40);

        Entity.RemoveComponent<SpriteAnimator>();
        animator = Entity.AddComponent(new SpriteAnimator() { LayerDepth = .5f });
        animator.AddAnimation("idle", Game.Atlas.GetAnimation("fountain"));
        animator.AddAnimation("bump", Game.Atlas.GetAnimation("fountain"));
        animator.Play("idle");
    }

    public new void OnTriggerEnter(Collider other, Collider local)
    {
        base.OnTriggerEnter(other, local);
        animator.Play("idle");
    }
}
