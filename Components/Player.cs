using System;
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
    private const float gravity = 0.1f;

    public Vector2 Velocity;
    Mover mover;

    public StateMachine<Player> fsm;
    public SpriteAnimator animator;

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        Entity.AddComponent(new BoxCollider(260, 260));
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

        animator = Entity.AddComponent(new SpriteAnimator());
        addSingleTextureAnimation("3-slide");
        addSingleTextureAnimation("3-rise");
        addSingleTextureAnimation("3-charge_throw");
        addSingleTextureAnimation("2-fly");
        addSingleTextureAnimation("2-charge_throw");
        addSingleTextureAnimation("1-fly");
    }

    public void Update()
    {
        fsm.Update(Time.DeltaTime);
    }
    public void PhysicalUpdate()
    {
        Velocity.Y += gravity;
        if (mover.Move(Velocity, out var collisionResult))
        {
            WorldObject other = collisionResult.Collider.Entity.GetComponent<WorldObject>();
            if (other != null)
                Velocity = other.Collision(this, collisionResult.Normal);
            Ground ground = collisionResult.Collider.Entity.GetComponent<Ground>();
            if (ground != null)
            {
                Vector2 tangent = new Vector2(collisionResult.Normal.Y, -collisionResult.Normal.X);
                Velocity = Vector2.Dot(tangent, Velocity) * tangent;
            }
        }
    }
    public void Throw(float velocity, float angle = (float) Math.PI / 4)
    {
        Velocity = velocity * new Vector2((float)Math.Cos(angle), -(float)Math.Sin(angle));
    }
    private void addSingleTextureAnimation(string name)
    {
        var texture = Entity.Scene.Content.Load<Texture2D>("player/" + name);
        var sprite = new Sprite(texture);
        animator.AddAnimation(name, new[] { sprite });
    }
    public bool isThrowInputGiven()
    {
        return Keyboard.GetState().IsKeyDown(Keys.Space);
    }
}