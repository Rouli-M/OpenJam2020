using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenJam2020.Components
{
    public class DroppedDino : Component
    {
        int dinoId;

        public DroppedDino(int dinoId)
        {
            this.dinoId = dinoId;
        }

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();

            if (dinoId == 0)
            {
                Entity.AddComponent(new SpriteAnimator())
                    .AddAnimation("canon_shoot", Game.Atlas.GetAnimation("canon_shoot"))
                    .Play("canon_shoot", SpriteAnimator.LoopMode.Once);
            }
            else if (dinoId == 3) AddRenderer("3-throw");
            else if (dinoId == 2) AddRenderer("2-throw");
        }

        private void AddRenderer(string spriteName)
        {
            Entity.AddComponent(new SpriteRenderer(Game.Atlas.GetSprite(spriteName)) { LayerDepth = .5f });
        }
    }
}
