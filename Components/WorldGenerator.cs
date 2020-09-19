using Nez;
using Microsoft.Xna.Framework;

class WorldGenerator : Component
{
    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();
        generate();
    }

    public void generate()
    {
        var newComponent = Entity.Scene.CreateEntity("aa").AddComponent<Bumper>();
        newComponent.Transform.Position = new Vector2(1500, -40);
        newComponent.Transform.Parent = this.Transform;
    }
}
