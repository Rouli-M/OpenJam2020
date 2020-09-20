using Nez;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Forms;

class WorldGenerator : Component, IUpdatable
{
    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();
    }

    public void generate(float xPosition)
    {
        if (Random.NextInt(64) == 0)
        {
            int type = Random.NextInt(2);
            int yPosition;
            switch (type) {
                case 0:
                    yPosition = -62;
                    if (checkPosition(new Vector2(xPosition, yPosition)))
                    {
                        var newComponent = Entity.Scene.CreateEntity("bumper").AddComponent<Bumper>();
                        newComponent.Transform.Position = new Vector2(xPosition, yPosition);
                        newComponent.Transform.Parent = this.Transform;
                    }
                    break;
                case 1:
                    yPosition = 455 - Random.NextInt(1000);
                    if (checkPosition(new Vector2(xPosition, yPosition - 517)))
                    {
                        var newComponent = Entity.Scene.CreateEntity("tall_bumper").AddComponent<TallBumper>();
                        newComponent.Transform.Position = new Vector2(xPosition, yPosition);
                        newComponent.Transform.Parent = this.Transform;
                    }
                    break;
            }
        }

        if (Random.NextInt(16) == 0)
        {
            int type = Random.NextInt(2);
            switch (type)
            {
                case 0:
                    if (checkPosition(new Vector2(xPosition, 0)))
                    {
                        var newComponent = Entity.Scene.CreateEntity("arbre").AddComponent(new Decor("arbre1"));
                        newComponent.Transform.Position = new Vector2(xPosition, 0);
                        newComponent.Transform.Parent = this.Transform;
                    }
                    break;
                case 1:
                    if (checkPosition(new Vector2(xPosition, 0), 450))
                    {
                        var newComponent = Entity.Scene.CreateEntity("arbre").AddComponent(new Decor("arbre2"));
                        newComponent.Transform.Position = new Vector2(xPosition, 0);
                        newComponent.Transform.Parent = this.Transform;
                    }
                    break;
            }
        }
    }

    private bool checkPosition(Vector2 position, int width = 250)
    {
        List<WorldObject> worldObjects = Entity.Scene.FindComponentsOfType<WorldObject>();
        foreach (var o in worldObjects)
        {
            if (Vector2.Distance(o.Transform.Position, position) < width)
                return false;
        }
        return true;
    }

    public void Update()
    {
        generate(Entity.Scene.FindComponentOfType<Player>().Transform.Position.X + 4500);
    }
}
