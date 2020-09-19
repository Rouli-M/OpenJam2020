using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using static Constants;

public class Ground : Component
{
    const int GROUND_HEIGHT = 100;

    int groundWidth = DESIGN_WIDTH * 100;

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        Entity.AddComponent(new BoxCollider(groundWidth, GROUND_HEIGHT));
        Transform.Position = new Vector2(DESIGN_WIDTH / 2, DESIGN_HEIGHT - GROUND_HEIGHT / 2);

        Entity.AddTiledTexture("textures/ground");
        Entity.AddTiledTexture("textures/ground_top");
    }
}