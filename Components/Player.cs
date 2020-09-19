﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.AI.FSM;
using Nez.Sprites;
using Nez.Textures;

public class Player : Component, IUpdatable
{
    private const float gravity = 360f;
    private const float groundFriction = 300f;

    public Vector2 Velocity;
    Mover mover;

    public StateMachine<Player> fsm;
    public SpriteAnimator animator;
    SpriteAtlas atlas;

    public readonly Point Box3 = new Point(260, 260);
    public readonly Point Box2 = new Point(200, 200);
    public readonly Point Box1 = new Point(100, 100);

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        Entity.AddComponent(new BoxCollider(Box3.X, Box3.Y));
        mover = Entity.AddComponent(new Mover());

        Transform.Position = new Vector2(0, -80);
        Velocity = new Vector2(0, 0);

        fsm = new StateMachine<Player>(this, new NotThrownState());
        fsm.AddState(new FallenState());
        fsm.AddState(new Flying_3State());
        fsm.AddState(new Flying_2State());
        fsm.AddState(new Flying_1State());
        fsm.AddState(new Sliding_3State());
        fsm.AddState(new Sliding_2State());
        fsm.AddState(new Sliding_1State());
        fsm.AddState(new ThrowingState());
        fsm.AddState(new Throwing_3State());
        fsm.AddState(new Throwing_2State());

        atlas = Entity.Scene.Content.LoadSpriteAtlas("Content/bundle.atlas");
        animator = Entity.AddComponent(new SpriteAnimator());
        AddSingleTextureAnimation("3-slide");
        AddSingleTextureAnimation("3-rise");
        AddSingleTextureAnimation("3-fall");
        AddSingleTextureAnimation("3-charge_throw");
        AddSingleTextureAnimation("2-fly");
        AddSingleTextureAnimation("2-slide");
        AddSingleTextureAnimation("2-charge_throw");
        AddSingleTextureAnimation("1-fly");
        AddAtlasAnimation("1-slide");
    }

    public void Update()
    {
        fsm.Update(Time.DeltaTime);
    }
    public void PhysicalUpdate(float TimeScale = 1)
    {
        Time.TimeScale = TimeScale;
        Velocity.Y += gravity * Time.DeltaTime;
        if (mover.Move(Velocity * Time.DeltaTime, out var collisionResult))
        {
            WorldObject other = collisionResult.Collider.Entity.GetComponent<WorldObject>();
            if (other != null)
                Velocity = other.Collision(this, collisionResult.Normal);
            Ground ground = collisionResult.Collider.Entity.GetComponent<Ground>();
            if (ground != null)
            {
                // gestion collision
                Vector2 tangent = new Vector2(collisionResult.Normal.Y, -collisionResult.Normal.X);
                Velocity = Vector2.Dot(tangent, Velocity) * tangent;

                // frottements
                if (Velocity.Length() < groundFriction * Time.DeltaTime)
                {
                    Velocity = Vector2.Zero;
                    if (fsm.CurrentState is Flying_1State)
                        fsm.ChangeState<FallenState>();
                }
                else
                    Velocity -= groundFriction * Time.DeltaTime * Vector2.Normalize(Velocity);

                if (fsm.CurrentState is Flying_1State state1)
                    state1.slide();
                if (fsm.CurrentState is Flying_2State state2)
                    state2.slide();
                if (fsm.CurrentState is Flying_3State state3)
                    state3.slide();
            }
        }
    }
    public void Throw(float velocity, float angle = (float)Math.PI / 4)
    {
        Velocity = velocity * new Vector2((float)Math.Cos(angle), -(float)Math.Sin(angle));
    }
    private void AddAtlasAnimation(string name)
    {
        var animation = atlas.GetAnimation(name);
        animator.AddAnimation(name, animation);
    }
    private void AddSingleTextureAnimation(string name)
    {
        var sprite = atlas.GetSprite(name);
        animator.AddAnimation(name, new[] { sprite });
    }

    public bool IsThrowInputGiven()
    {
        return Keyboard.GetState().IsKeyDown(Keys.Space);
    }
}