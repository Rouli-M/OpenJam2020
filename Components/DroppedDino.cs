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

            if (DinoID == 0) Entity.AddComponent(new SpriteRenderer(Game.Atlas.GetSprite("canon_empty")));
            if (DinoID == 3) Entity.AddComponent(new SpriteRenderer(Game.Atlas.GetSprite("3-throw")));
            if (DinoID == 2) Entity.AddComponent(new SpriteRenderer(Game.Atlas.GetSprite("2-throw")));
        }
    }
}
