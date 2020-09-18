using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Nez;

namespace OpenJam2020
{
    public class Player : Component, IUpdatable
    {
        private const float gravity = 0.1f;

        Vector2 Velocity;
        Mover mover;

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();

            Entity.AddComponent(new BoxCollider(200, 200));
            mover = Entity.AddComponent(new Mover());

            Transform.Position = new Vector2(0, 111);
            Velocity = new Vector2(3, 0);
        }

        public void Update()
        {
            Velocity.Y += gravity;
            if (mover.Move(Velocity, out var collisionResult))
            {
                Vector2 tangent = new Vector2(collisionResult.Normal.Y, -collisionResult.Normal.X);
                Velocity = Vector2.Dot(tangent, Velocity) * tangent;
            }
        }
    }
}
