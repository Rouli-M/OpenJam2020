using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;

public static class EntityExtensions
{
    public static TiledSpriteRenderer AddTiledTexture(this Entity entity, string textureName)
    {
        var texture = entity.Scene.Content.Load<Texture2D>(textureName);
        var renderer = entity.AddComponent(new TiledSpriteRenderer(texture));
        renderer.Width = Constants.DESIGN_WIDTH * 10; // Take a random length for the level.
        renderer.OriginNormalized = new Vector2(.5f, .5f);
        return renderer;
    }
}