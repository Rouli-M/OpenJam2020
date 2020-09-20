using Microsoft.Xna.Framework;
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
        var basee = Entity.AddComponent(new SpriteRenderer(Game.Atlas.GetSprite("base")) { LayerDepth = .6f });
        basee.LocalOffset = new Vector2(-1205 + DESIGN_WIDTH / 2, -475);

        Transform.Position = new Vector2(0, 0);

        Entity.AddTiledTexture("root/ground", .2f, Constants.PREHISTORY_LENGHT, GROUND_HEIGHT * 100);
        Entity.AddTiledTexture("root/ground_top", .1f, Constants.PREHISTORY_LENGHT);

        var ground2 = Entity.AddTiledTexture("root/ground2", .2f, Constants.MIDDLEAGE_LENGHT, GROUND_HEIGHT * 100);
        ground2.OriginNormalized = new Vector2(0, 0);
        ground2.LocalOffset = new Vector2(PREHISTORY_LENGHT_END, 0);

        var ground2Top = Entity.AddTiledTexture("root/ground2_top", .1f, Constants.MIDDLEAGE_LENGHT);
        ground2Top.OriginNormalized = new Vector2(0, 0);
        ground2Top.LocalOffset = new Vector2(PREHISTORY_LENGHT_END, 0);
    }
}
