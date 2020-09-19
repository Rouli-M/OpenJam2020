using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenJam2020.Components
{
    public class DroppedDino:Component, IUpdatable
    {
        int DinoID;

        public DroppedDino(int DinoID)
        {
            this.DinoID = DinoID;
        }

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();

            if (DinoID == 3) AddSingleTextureAnimation("player/3-throw");
            if (DinoID == 2) AddSingleTextureAnimation("player/2-throw");
            Transform.Position = Entity.Scene.FindComponentOfType<Player>().Transform.Position;
        }

        public void Update()
        {
           // throw new NotImplementedException();
        }
        private void AddSingleTextureAnimation(string name)
        {
            Texture2D texture = Entity.Scene.Content.Load<Texture2D>(name);

            Entity.AddComponent(new SpriteRenderer(texture));
        }
    }
}
