﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Nez;
using Nez.Sprites;
using System;

class Bumper : WorldObject, ITriggerListener
{
    protected const float minBumpVelocity = 600f;
    protected readonly Vector2 bumpDirection = Vector2.Normalize(new Vector2(0f, -1));
    protected SpriteAnimator animator;
    protected SoundEffect bounceSound;

    public override void OnAddedToEntity()
    {
        var collider = Entity.AddComponent(new BoxCollider(-110, -10, 220, 80));
        collider.IsTrigger = true;

        animator = Entity.AddComponent(new SpriteAnimator() { LayerDepth = .5f });
        animator.AddAnimation("idle", new[] { Game.Atlas.GetSprite("champi1") });
        animator.AddAnimation("bump", Game.Atlas.GetAnimation("champi"));

        bounceSound = Core.Content.Load<SoundEffect>("bounce");

        base.OnAddedToEntity();
    }

    public void OnTriggerEnter(Collider other, Collider local)
    {
        var player = other.GetComponent<Player>();
        player.Velocity += Math.Max(2 * Vector2.Dot(-player.Velocity, bumpDirection), minBumpVelocity) * bumpDirection;
        
        bounceSound.Play();

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
