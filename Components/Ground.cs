using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using static Constants;

public class Ground : Component
{
    const int GROUND_HEIGHT = 100;

    int groundWidth = DESIGN_WIDTH * 100;

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        Entity.AddComponent(new BoxCollider(0, 0, groundWidth, GROUND_HEIGHT));
        var basee = Entity.AddComponent(new SpriteRenderer(Entity.Scene.Content.Load<Texture2D>("root/base")));
        basee.LocalOffset = new Vector2(-1194, -475);

        Transform.Position = new Vector2(0, GROUND_HEIGHT / 2);

        Entity.AddTiledTexture("root/ground", 0.1f, groundWidth, GROUND_HEIGHT * 100);
        Entity.AddTiledTexture("root/ground_top", 0, groundWidth);
    }
}
