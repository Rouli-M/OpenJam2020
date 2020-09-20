using Nez;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

class WorldGenerator : Component, IUpdatable
{
    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();
    }

    public void generate(float xPosition)
    {
        if (Time.DeltaTime > 0 && Random.NextInt((int)(1/Time.DeltaTime)) == 0)
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
    }

    private bool checkPosition(Vector2 position)
    {
        List<WorldObject> worldObjects = Entity.Scene.FindComponentsOfType<WorldObject>();
        foreach (var o in worldObjects)
        {
            if (Vector2.Distance(o.Transform.Position, position) < 250)
                return false;
        }
        return true;
    }

    public void Update()
    {
        generate(Entity.Scene.FindComponentOfType<Player>().Transform.Position.X + 4500);
    }
}
