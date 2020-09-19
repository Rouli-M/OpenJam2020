using Microsoft.Xna.Framework;
using Nez;
using static Constants;

public class Ground : Component
{
    const int GROUND_HEIGHT = 100;

    int groundWidth = DESIGN_WIDTH * 100;

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        Entity.AddComponent(new BoxCollider(0, 0, groundWidth, GROUND_HEIGHT));
        Transform.Position = new Vector2(DESIGN_WIDTH / 2, GROUND_HEIGHT / 2);

        Entity.AddTiledTexture("textures/ground", groundWidth, GROUND_HEIGHT * 100);
        Entity.AddTiledTexture("textures/ground_top", groundWidth);
    }
}