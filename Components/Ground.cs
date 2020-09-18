using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using static Constants;

public class Ground : Component
{
    private const int GROUND_HEIGHT = 100;

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        Entity.AddComponent(new BoxCollider(DESIGN_WIDTH, GROUND_HEIGHT));
        Transform.Position = new Vector2(DESIGN_WIDTH / 2, DESIGN_HEIGHT - GROUND_HEIGHT / 2);

        AddTiledTexture("textures/ground_top");
        AddTiledTexture("textures/ground");
    }

    private void AddTiledTexture(string textureName)
    {
        var texture = Entity.Scene.Content.Load<Texture2D>(textureName);
        var renderer = Entity.AddComponent(new TiledSpriteRenderer(texture));
        renderer.Width = DESIGN_WIDTH * 10; // Take a random length for the level.
        renderer.OriginNormalized = new Vector2(.5f, .5f);
    }
}