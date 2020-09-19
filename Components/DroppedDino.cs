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
        int DinoID;

        public DroppedDino(int DinoID)
        {
            this.DinoID = DinoID;
        }

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();

            if (DinoID == 0) AddSingleTextureAnimation("root/canon_empty");
            if (DinoID == 3) AddSingleTextureAnimation("root/3-throw");
            if (DinoID == 2) AddSingleTextureAnimation("root/2-throw");
        }

        private void AddSingleTextureAnimation(string name)
        {
            Texture2D texture = Entity.Scene.Content.Load<Texture2D>(name);
            Entity.AddComponent(new SpriteRenderer(texture));
        }
    }
}
