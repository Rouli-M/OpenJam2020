using Microsoft.Xna.Framework;
using Nez;

public class CameraScroll : Component, IUpdatable
{
    float scrollSpeed = 300f;

    public void Update()
    {
        Transform.Position += new Vector2(scrollSpeed * Time.DeltaTime, 0);
    }
}