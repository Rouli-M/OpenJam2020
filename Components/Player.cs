using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.AI.FSM;
using Nez.Sprites;

public class Player : Component, IUpdatable
{
    private const float gravity = 0.1f;

    public Vector2 Velocity;
    Mover mover;
    
    public StateMachine<Player> fsm;

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        Entity.AddComponent(new BoxCollider(260, 260));
        mover = Entity.AddComponent(new Mover());

        fsm = new StateMachine<Player>(this, new NotThrownState());
        fsm.AddState(new FallenState());
        fsm.AddState(new Flying_3State());

        var texture = Entity.Scene.Content.Load<Texture2D>("player/3-rise");
        Entity.AddComponent(new SpriteRenderer(texture));

        Transform.Position = new Vector2(0, -80);
        Velocity = new Vector2(0, 0);
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
    public void Throw(float angle, float velocity)
    {
        Velocity = velocity * new Vector2((float)Math.Cos(angle), -(float)Math.Sin(angle));
    }
}