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
    private const float gravity = 360f;

    public Vector2 Velocity;
    Mover mover;

    public StateMachine<Player> fsm;
    public SpriteAnimator animator;
    SpriteAtlas atlas;

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

        atlas = Entity.Scene.Content.LoadSpriteAtlas("Content/bundle.atlas");
        animator = Entity.AddComponent(new SpriteAnimator());

        AddSingleTextureAnimation("3-slide");
        AddSingleTextureAnimation("3-rise");
        AddSingleTextureAnimation("3-charge_throw");
        AddSingleTextureAnimation("2-fly");
        AddSingleTextureAnimation("2-charge_throw");
        AddSingleTextureAnimation("1-fly");
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
                Vector2 tangent = new Vector2(collisionResult.Normal.Y, -collisionResult.Normal.X);
                Velocity = Vector2.Dot(tangent, Velocity) * tangent;
            }
        }
    }
    public void Throw(float velocity, float angle = (float)Math.PI / 4)
    {
        Velocity = velocity * new Vector2((float)Math.Cos(angle), -(float)Math.Sin(angle));
    }
    private void AddSingleTextureAnimation(string name)
    {
        var sprite = atlas.GetSprite(name);
        animator.AddAnimation(name, new[] { sprite });
    }
    
    public bool isThrowInputGiven()
    {
        return Keyboard.GetState().IsKeyDown(Keys.Space);
    }
}