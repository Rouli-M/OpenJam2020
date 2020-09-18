using Microsoft.Xna.Framework;
using Nez;
using static Constants;

public class Ground : Component
{
    private const int GROUND_HEIGHT = 30;

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();
        Entity.AddComponent(new BoxCollider(DESIGN_WIDTH, GROUND_HEIGHT));
        Transform.Position = new Vector2(DESIGN_WIDTH / 2, DESIGN_HEIGHT - GROUND_HEIGHT / 2);
    }
}