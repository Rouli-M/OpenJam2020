using Microsoft.Xna.Framework;
using Nez;

public class Landscape : Component
{
    Layer[] layers;

    struct Layer
    {
        public Transform transform;
        public float multiplier;

        public Layer(Transform transform, float multiplier)
        {
            this.transform = transform;
            this.multiplier = multiplier;
        }
    }

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        var sky = Entity.Scene.CreateEntity("sky");
        sky.Parent = Transform;
        sky.Transform.Position = new Vector2(0, 300);

        var renderer = sky.AddTiledTexture("textures/sky", Constants.DESIGN_WIDTH * 100, Constants.DESIGN_HEIGHT * 50);
        renderer.OriginNormalized = new Vector2(.5f, 1);
        renderer.LayerDepth = 10;

        layers = new[] { new Layer(sky.Transform, .8f) };
    }

    public void Scroll(Vector2 delta)
    {
        if (layers == null) return;

        layers[0].transform.Position += layers[0].multiplier * delta;
    }
}