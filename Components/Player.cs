using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

namespace OpenJam2020
{
    public class Player : Component, IUpdatable
    {
        private const float gravity = 0.01f;

        public Vector2 Velocity;
        Mover mover;
        bool thrown;

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();

            Entity.AddComponent(new BoxCollider(260, 260));
            mover = Entity.AddComponent(new Mover());

            var texture = Entity.Scene.Content.Load<Texture2D>("player/3-rise");
            Entity.AddComponent(new SpriteRenderer(texture));

            Transform.Position = new Vector2(0, 0);
            Velocity = new Vector2(10, 0);

            thrown = false;
        }

        public void Update()
        {
            if (thrown)
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
            } else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    Velocity = new Vector2(20 * (float)Math.Cos(Math.PI / 4), -20 * (float)Math.Sin(Math.PI / 4));
                    thrown = true;
                }
            }
        }
    }
}
