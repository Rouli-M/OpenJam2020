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

        var sky = AddSky();
        var trees = AddTrees();
        var space = AddSpace();

        layers = new[] {
            new Layer(space.Transform, .75f),
            new Layer(sky.Transform, .75f),
            new Layer(trees.Transform, 0.1f)
        };
    }

    private Entity AddSky()
    {
        var entity = Entity.Scene.CreateEntity("sky");
        entity.Parent = Transform;
        entity.Transform.Position = new Vector2(0, 300);

        var renderer = entity.AddTiledTexture("root/sky", 0.99f, Constants.DESIGN_WIDTH * 100, Constants.SKY_HEIGHT);
        renderer.OriginNormalized = new Vector2(.5f, 1);

        var topRenderer = entity.AddTiledTexture("root/space_to_sky", .99f, Constants.DESIGN_WIDTH * 100);
        topRenderer.OriginNormalized = new Vector2(.5f, 1);
        topRenderer.LocalOffset = new Vector2(0, -renderer.Origin.Y);
        return entity;
    }

    private Entity AddSpace()
    {
        var entity = Entity.Scene.CreateEntity("space");
        entity.Parent = Transform;
        entity.Transform.Position = new Vector2(0, 300);

        var renderer = entity.AddTiledTexture("root/space", 1, Constants.DESIGN_WIDTH * 100, Constants.SPACE_HEIGHT);
        renderer.OriginNormalized = new Vector2(.5f, 1);
        return entity;
    }

    private Entity AddTrees()
    {
        var entity = Entity.Scene.CreateEntity("trees");
        entity.Parent = Transform;
        entity.Transform.Position = new Vector2(0, 300);

        var treesRenderer = entity.AddTiledTexture("root/bg1", .9f, Constants.DESIGN_WIDTH * 100, Constants.TREES_HEIGHT);
        treesRenderer.OriginNormalized = new Vector2(.5f, 1);

        var topRenderer = entity.AddTiledTexture("root/bg1-top", .8f, Constants.DESIGN_WIDTH * 100);
        topRenderer.OriginNormalized = new Vector2(.5f, 1);
        topRenderer.LocalOffset = new Vector2(0, -treesRenderer.Origin.Y);

        return entity;
    }

    public void Scroll(Vector2 delta)
    {
        if (layers == null) return;

        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].transform.Position += layers[i].multiplier * delta;

        }
        //layers[0].transform.Position = layers[0].multiplier * PositionX;
    }
}