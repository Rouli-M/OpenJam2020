using Microsoft.Xna.Framework;
using Nez;
using Nez.Particles;

public class Confettis : Component
{
    public ParticleEmitter[] confettiParticles;

    public override void OnAddedToEntity()
    {
        base.OnAddedToEntity();

        var offset = 900;

        confettiParticles = new[] {
            Entity.AddComponent(new ParticleEmitter(Particles.MakeConfettiParticlesConfig(true)) { LocalOffset = new Vector2(-offset, 0)}),
            Entity.AddComponent(new ParticleEmitter(Particles.MakeConfettiParticlesConfig(false)) { LocalOffset = new Vector2(offset, 0)}),
        };

        Transform.Position = Game.player.Transform.Position + new Vector2(0, 100);
    }
}