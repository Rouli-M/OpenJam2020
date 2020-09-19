using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.AI.FSM;
using Nez.Sprites;
using Nez.Textures;

public class Player : Component, IUpdatable
{
    private const float gravity = 250f;
    private float groundFriction = 300f;

    public Vector2 Velocity;
    Mover mover;

    public StateMachine<Player> fsm;
    public SpriteAnimator animator;

    public SoundEffect charge_canon_sound, success, throw_sound, hold_sound;

    public readonly Point Box3 = new Point(260, 260);
    public readonly Point Box2 = new Point(200, 200);
    public readonly Point Box1 = new Point(100, 100);

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        var collider = Entity.AddComponent(new BoxCollider(Box3.X, Box3.Y));
        collider.ShouldColliderScaleAndRotateWithTransform = false;
        mover = Entity.AddComponent(new Mover());

        Transform.Position = new Vector2(0, -130);
        Velocity = new Vector2(0, 0);

        animator = Entity.AddComponent(new SpriteAnimator());
        AddSingleTextureAnimation("canon");
        AddSingleTextureAnimation("canon_loaded");
        AddSingleTextureAnimation("3-rise");
        AddSingleTextureAnimation("3-fall");
        AddSingleTextureAnimation("3-top");
        AddSingleTextureAnimation("3-slide");
        AddSingleTextureAnimation("3-charge_throw");
        AddSingleTextureAnimation("2-fly");
        AddSingleTextureAnimation("2-slide");
        AddSingleTextureAnimation("2-charge_throw");
        AddSingleTextureAnimation("1-fly");
        AddAtlasAnimation("1-slide");
        AddAtlasAnimation("1-win");

        fsm = new StateMachine<Player>(this, new NotThrownState());
        fsm.AddState(new HatchState());
        fsm.AddState(new Flying_3State());
        fsm.AddState(new Flying_2State());
        fsm.AddState(new Flying_1State());
        fsm.AddState(new Sliding_3State());
        fsm.AddState(new Sliding_2State());
        fsm.AddState(new Sliding_1State());
        fsm.AddState(new ThrowingState());
        fsm.AddState(new Throwing_3State());
        fsm.AddState(new Throwing_2State());

        charge_canon_sound = Core.Content.Load<SoundEffect>("charge_up");
        throw_sound = Core.Content.Load<SoundEffect>("throw1");
        success = Core.Content.Load<SoundEffect>("success");
        hold_sound = Core.Content.Load<SoundEffect>("hold");
    }

    internal bool IsThrowing()
    {
        return (fsm.CurrentState is Throwing_2State || fsm.CurrentState is Throwing_3State);
    }

    internal int DinoCount()
    {
        if (fsm.CurrentState is Flying_1State || fsm.CurrentState is Sliding_1State) return 1;
        else if (fsm.CurrentState is Flying_2State || fsm.CurrentState is Sliding_2State || fsm.CurrentState is Throwing_2State) return 2;
        else if (fsm.CurrentState is Flying_3State || fsm.CurrentState is Sliding_3State || fsm.CurrentState is Throwing_3State) return 3;
        return 0;
    }

    public void Update()
    {
        fsm.Update(Time.DeltaTime);
    }
    public void PhysicalUpdate()
    {
        Velocity.Y += gravity * Time.DeltaTime;

        if (fsm.CurrentState is Flying_2State || fsm.CurrentState is Flying_1State) Transform.Rotation = MathF.Atan2(Velocity.Y, Velocity.X);
        else Transform.Rotation = 0f;

        if (fsm.CurrentState is Sliding_1State) groundFriction = 100;
        else groundFriction = 250f;

        if (mover.Move(Velocity * Time.DeltaTime, out var collisionResult))
        {
            WorldObject other = collisionResult.Collider.Entity.GetComponent<WorldObject>();
            if (other != null)
            {
                // gestion collision
                Vector2 tangent = new Vector2(collisionResult.Normal.Y, -collisionResult.Normal.X);
                Velocity = Vector2.Dot(tangent, Velocity) * tangent;
            }
            Ground ground = collisionResult.Collider.Entity.GetComponent<Ground>();
            if (ground != null &&Vector2.Dot(collisionResult.Normal, Velocity) < 0)
            {

                Vector2 tangent = new Vector2(collisionResult.Normal.Y, -collisionResult.Normal.X);
                Velocity = Vector2.Dot(tangent, Velocity) * tangent;


                // frottements
                if (Velocity.Length() < groundFriction * Time.DeltaTime)
                {
                    Velocity = Vector2.Zero;
                    if (fsm.CurrentState is Flying_1State || fsm.CurrentState is Sliding_1State)
                    {
                        fsm.ChangeState<HatchState>();
                        animator.Play("1-win");
                    }
                }
                else
                    Velocity -= groundFriction * Time.DeltaTime * Vector2.Normalize(Velocity);

                if (fsm.CurrentState is Flying_1State state1)
                    fsm.ChangeState<Sliding_1State>();
                if (fsm.CurrentState is Flying_2State state2)
                    fsm.ChangeState<Sliding_2State>();
                if (fsm.CurrentState is Flying_3State state3)
                    fsm.ChangeState<Sliding_3State>();
            }
            else
            {

                if (fsm.CurrentState is Sliding_1State state1)
                    fsm.ChangeState<Flying_1State>();
                if (fsm.CurrentState is Sliding_2State state2)
                    fsm.ChangeState<Flying_2State>();
                if (fsm.CurrentState is Sliding_3State state3)
                    fsm.ChangeState<Flying_3State>();
            }
        }
    }

    public Vector2 Throw(float speed, float angle = MathF.PI / 4)
    {
        throw_sound.Play();
        var direction = new Vector2(MathF.Cos(angle), -MathF.Sin(angle));
        this.Velocity = new Vector2(0.35f * this.Velocity.X, -0.35f * this.Velocity.X) + speed * direction;
        return direction;
    }

    private void AddAtlasAnimation(string name)
    {
        var animation = Game.Atlas.GetAnimation(name);
        animator.AddAnimation(name, animation);
    }

    private void AddSingleTextureAnimation(string name)
    {
        var sprite = Game.Atlas.GetSprite(name);
        animator.AddAnimation(name, new[] { sprite });
    }

    public bool IsThrowInputGiven()
    {
        if (Game.IsPaused) return false;
        return Keyboard.GetState().IsKeyDown(Keys.Space);
    }
}