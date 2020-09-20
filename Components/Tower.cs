using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Nez;
using Nez.Sprites;
using Nez.Textures;

class Part
{
    public Vector2 velocity;
    public SpriteRenderer renderer;
    private Entity entity;

    public bool DecrementAlpha()
    {
        renderer.Color = Color.FromNonPremultiplied(255, 255, 255, renderer.Color.A - 6);

        var hidden = renderer.Color.A <= 0;

        if (hidden)
            entity.RemoveComponent(renderer);

        return hidden;
    }

    public Part(Entity entity, float y, Sprite sprite)
    {
        this.entity = entity;
        renderer = entity.AddComponent(new SpriteRenderer(sprite) { LocalOffset = new Vector2(0, y) });
        renderer.OriginNormalized = new Vector2(.5f, 1);

        var angle = Random.NextAngle();
        velocity = Mathf.AngleToVector(angle, 4 + Random.NextFloat(3));
    }
}

class Tower : WorldObject, ITriggerListener
{
    protected SoundEffect breakSound;
    private Part[] parts;
    private Vector2 gravity = new Vector2(0, .3f);

    public override void OnAddedToEntity()
    {
        var collider = Entity.AddComponent(new BoxCollider(-100, -630, 200, 1370));
        collider.IsTrigger = true;

        var part = Game.Atlas.GetSprite("tower_particle");
        var top = Game.Atlas.GetSprite("tower_top");

        var offset = 750;
        var height = -165;

        parts = new[] {
            new Part(Entity, offset + height * 7, top),
            new Part(Entity, offset + height * 6, part),
            new Part(Entity, offset + height * 5, part),
            new Part(Entity, offset + height * 4, part),
            new Part(Entity, offset + height * 3, part),
            new Part(Entity, offset + height * 2, part),
            new Part(Entity, offset + height * 1, part),
            new Part(Entity, offset + height * 0, part)
        };

        breakSound = Core.Content.Load<SoundEffect>("stomp");

        base.OnAddedToEntity();
    }

    public void PlayAnimation()
    {
        Core.Schedule(.01f, true, this, (timer) =>
        {
            var end = false;

            foreach (var part in parts)
            {
                part.velocity += gravity;
                part.renderer.LocalOffset += part.velocity;

                if (part.DecrementAlpha())
                    end = true;
            }

            if (end)
                timer.Stop();
        });
    }

    public void OnTriggerEnter(Collider other, Collider local)
    {
        var player = other.GetComponent<Player>();
        player.Velocity.X /= 2;

        PlayAnimation();

        breakSound.Play();
    }

    public void OnTriggerExit(Collider other, Collider local) { }
}
