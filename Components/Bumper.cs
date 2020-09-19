using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;

class Bumper : WorldObject, ITriggerListener
{
    protected const float minBumpVelocity = 500f;
    protected readonly Vector2 bumpDirection = new Vector2(0, -1);
    protected SpriteAnimator animator;
    protected SoundEffect bounce_sound;

    public override void OnAddedToEntity()
    {
        var collider = Entity.AddComponent(new BoxCollider(-110, -10, 220, 80));
        collider.IsTrigger = true;

        SpriteAtlas atlas = Entity.Scene.Content.LoadSpriteAtlas("Content/bundle.atlas");
        animator = Entity.AddComponent(new SpriteAnimator() { LayerDepth = .5f });

        bounce_sound = Core.Content.Load<SoundEffect>("bounce");

        base.OnAddedToEntity();
    }

    public void OnTriggerEnter(Collider other, Collider local)
    {
        var player = other.GetComponent<Player>();
        player.Velocity += MathF.Max(2 * Vector2.Dot(-player.Velocity, bumpDirection), minBumpVelocity) * bumpDirection;
        
        bounce_sound.Play();

        if (!player.IsThrowing())
        {
            Debug.Log("Bumper bounced player : dino detected : " + player.DinoCount().ToString());
            if (player.DinoCount() == 1) 
                player.fsm.ChangeState<Flying_1State>();
            else if (player.DinoCount() == 2) 
                player.fsm.ChangeState<Flying_2State>();
            else if (player.DinoCount() == 3) 
                player.fsm.ChangeState<Flying_3State>();
        }
        animator.Play("bump", SpriteAnimator.LoopMode.Once);
    }

    public void OnTriggerExit(Collider other, Collider local) { }
}
