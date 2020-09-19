using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;

public static class EntityExtensions
{
    public static TiledSpriteRenderer AddTiledTexture(this Entity entity, string textureName, float depth, int width)
    {
        return entity.AddTiledTexture(textureName, depth, width, null);
    }

    public static TiledSpriteRenderer AddTiledTexture(this Entity entity, string textureName, float depth, int width, int? height)
    {
        var texture = entity.Scene.Content.Load<Texture2D>(textureName);
        var renderer = entity.AddComponent(new TiledSpriteRenderer(texture));
        renderer.Width = width;
        if (height.HasValue) renderer.Height = height.Value;
        renderer.OriginNormalized = new Vector2(.5f, 0);
        renderer.LayerDepth = depth;
        return renderer;
    }
}