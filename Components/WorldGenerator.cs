using Nez;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

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
            if (xPosition < Constants.PREHISTORY_LENGHT_END)
                generatePrehistoricObject(xPosition);
            else if (xPosition < Constants.PREHISTORY_LENGHT_END + Constants.MIDDLEAGE_LENGHT)
                generateMiddleAgeObject(xPosition);
            else
                generateFutureObjects(xPosition);
            
                
        }

        if (Random.NextInt((int)((300 + 400) / probaMultiplier)) == 0)
        {
            if (xPosition < Constants.PREHISTORY_LENGHT_END)
                generatePrehistoricDecor(xPosition);
            else if (xPosition < Constants.PREHISTORY_LENGHT_END + Constants.MIDDLEAGE_LENGHT)
                generateMiddleAgeDecor(xPosition);
            else
                generateFutureDecor(xPosition);
        }

        if (Random.NextInt((int)(500 / probaMultiplier)) == 0)
        {
            generateSkyDecor(xPosition);
        }
    }

    void generatePrehistoricObject(float xPosition)
    {
        int type = Random.NextInt(3);
        int yPosition;
        switch (type)
        {
            case 0:
                tryToAddComponent("bumper", new Bumper(), new Vector2(xPosition, -62));
                break;
            case 1:
                yPosition = 455 - Random.NextInt(1000);
                if (checkPosition(new Vector2(xPosition, yPosition - 517)))
                {
                    tryToAddComponent("bumper", new Bumper(), new Vector2(xPosition, yPosition), int.MaxValue);
                }
                break;
            case 2:
                yPosition = -Constants.TREES_HEIGHT - Random.NextInt(2 * (Constants.SKY_HEIGHT - Constants.TREES_HEIGHT));
                tryToAddComponent("ptero", new Pterodactylus(), new Vector2(xPosition, yPosition));
                break;
        }
    }

    void generateMiddleAgeObject(float xPosition)
    {
        int type = Random.NextInt(2);
        int yPosition;
        switch (type)
        {
            case 0:
                tryToAddComponent("fountain", new Fountain(), new Vector2(xPosition, -62));
                break;
            case 1:
                yPosition = -500 - Random.NextInt(Constants.SKY_HEIGHT);
                tryToAddComponent("balloon", new Balloon(), new Vector2(xPosition, yPosition), 500);
                break;
        }
    }

    void generateFutureObjects(float xPosition)
    {
        int type = Random.NextInt(1);
        int yPosition;
        switch (type)
        {
            case 0:

                break;
        }
    }

    void generatePrehistoricDecor(float xPosition)
    {
        int type = Random.NextInt(2);
        switch (type)
        {
            case 0:
                tryToAddComponent("arbre", new Decor("arbre1"), new Vector2(xPosition, 0));
                break;
            case 1:
                tryToAddComponent("arbre", new Decor("arbre2"), new Vector2(xPosition, 0), 450);
                break;
        }
    }

    void generateMiddleAgeDecor(float xPosition)
    {
        int type = Random.NextInt(4);
        switch (type)
        {
            case 0:
                tryToAddComponent("maison", new Decor("maison"), new Vector2(xPosition, 0), 100);
                break;
            case 1:
                tryToAddComponent("puit", new Decor("puit"), new Vector2(xPosition, 0), 100);
                break;
            case 2:
                tryToAddComponent("tour", new Decor("tour"), new Vector2(xPosition, 0), 100);
                break;
            case 3:
                tryToAddComponent("tonneau", new Decor("tonneau"), new Vector2(xPosition, 0), 100);
                break;
        }
    }

    void generateFutureDecor(float xPosition)
    {
        int type = Random.NextInt(2);
        switch (type)
        {
            case 0:
                tryToAddComponent("future_maison", new Decor("future_home"), new Vector2(xPosition, 0), 100);
                break;
            case 1:
                tryToAddComponent("panneau futur", new Decor("panneau_future"), new Vector2(xPosition, 0), 100);
                break;
        }
    }

    void generateSkyDecor(float xPosition)
    {
        int type = Random.NextInt(2);
        int yPosition = -Constants.TREES_HEIGHT - Random.NextInt(2 * (Constants.SKY_HEIGHT - Constants.TREES_HEIGHT));
        switch (type)
        {
            case 0:
                tryToAddComponent("nuage", new Decor("nuage1"), new Vector2(xPosition, yPosition), 450);
                break;
            case 1:
                tryToAddComponent("nuage", new Decor("nuage2"), new Vector2(xPosition, yPosition), 450);
                break;
            case 2:
                tryToAddComponent("nuage", new Decor("nuage3"), new Vector2(xPosition, yPosition), 450);
                break;
        }
    }

    void tryToAddComponent(string name, Component component, Vector2 position, int width = 250)
    {
        if (checkPosition(position, width))
        {
            var newComponent = Entity.Scene.CreateEntity(name).AddComponent(component);
            newComponent.Transform.Position = position;
            newComponent.Transform.Parent = this.Transform;
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
