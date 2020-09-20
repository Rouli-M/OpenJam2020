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

    public void generate(float xPosition, float probaMultiplier)
    {
        if (probaMultiplier <= 0)
            return;
        if (Random.NextInt((int)(500 / probaMultiplier)) == 0)
        {
            int type = Random.NextInt(3);
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
                case 2:
                    yPosition = -Constants.TREES_HEIGHT - Random.NextInt(2 * (Constants.SKY_HEIGHT - Constants.TREES_HEIGHT));
                    if (checkPosition(new Vector2(xPosition, yPosition), 500))
                    {
                        var newComponent = Entity.Scene.CreateEntity("ptero").AddComponent<Pterodactylus>();
                        newComponent.Transform.Position = new Vector2(xPosition, yPosition);
                        newComponent.Transform.Parent = this.Transform;
                    }
                    break;
            }
        }

        if (Random.NextInt((int)(300 / probaMultiplier)) == 0)
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

        if (Random.NextInt((int)(500 / probaMultiplier)) == 0)
        {
            int type = Random.NextInt(2);
            int yPosition = -Constants.TREES_HEIGHT - Random.NextInt(2 * (Constants.SKY_HEIGHT - Constants.TREES_HEIGHT));
            switch (type)
            {
                case 0:
                    if (checkPosition(new Vector2(xPosition, yPosition), 450))
                    {
                        var newComponent = Entity.Scene.CreateEntity("nuage").AddComponent(new Decor("nuage1"));
                        newComponent.Transform.Position = new Vector2(xPosition, yPosition);
                        newComponent.Transform.Parent = this.Transform;
                    }
                    break;
                case 1:
                    if (checkPosition(new Vector2(xPosition, yPosition), 450))
                    {
                        var newComponent = Entity.Scene.CreateEntity("nuage").AddComponent(new Decor("nuage2"));
                        newComponent.Transform.Position = new Vector2(xPosition, yPosition);
                        newComponent.Transform.Parent = this.Transform;
                    }
                    break;
                case 2:
                    if (checkPosition(new Vector2(xPosition, yPosition), 450))
                    {
                        var newComponent = Entity.Scene.CreateEntity("nuage").AddComponent(new Decor("nuage3"));
                        newComponent.Transform.Position = new Vector2(xPosition, yPosition);
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
        var player = Entity.Scene.FindComponentOfType<Player>();
        generate(player.Transform.Position.X + 4500, player.Velocity.X * Time.DeltaTime);
    }
}
