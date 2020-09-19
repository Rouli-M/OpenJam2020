using Microsoft.Xna.Framework;
using Nez;
using System;

class Bumper : WorldObject, ITriggerListener
{
    private const float minBumpVelocity = 500f;
    private readonly Vector2 bumpDirection = new Vector2(0, -1);

    public override void OnAddedToEntity()
    {
        var collider = Entity.AddComponent(new BoxCollider(200, 200));
        collider.IsTrigger = true;

        base.OnAddedToEntity();
    }

    public void OnTriggerEnter(Collider other, Collider local)
    {
        var player = other.GetComponent<Player>();
        player.Velocity += MathF.Max(2 * Vector2.Dot(-player.Velocity, bumpDirection), minBumpVelocity) * bumpDirection;
    }

    public void OnTriggerExit(Collider other, Collider local)
    {

    }
}
