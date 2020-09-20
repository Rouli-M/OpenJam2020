using Microsoft.Xna.Framework;
using Nez.Particles;

public static class Particles
{

    public static ParticleEmitterConfig MakeFlyingParticlesConfig()
    {
        var config = new ParticleEmitterConfig();
        config.Sprite = Game.Atlas.GetSprite("circle_particle");

        var alpha = 255;
        var color = Color.FromNonPremultiplied(alpha, alpha, alpha, alpha);
        config.StartColor = color;
        config.StartColorVariance = Color.FromNonPremultiplied(0, 0, 0, 0);
        config.FinishColor = color;
        config.FinishColorVariance = Color.FromNonPremultiplied(0, 0, 0, 0);

        config.MaxParticles = 10000;
        config.EmissionRate = 20;
        config.EmitterType = ParticleEmitterType.Gravity;
        config.Duration = -1;
        config.StartParticleSize = 30;
        // config.BlendFuncSource = Microsoft.Xna.Framework.Graphics.Blend.SourceColor;
        // config.BlendFuncDestination = Microsoft.Xna.Framework.Graphics.Blend.SourceColor;
        // config.FinishParticleSize = 100;
        // config.MinRadius = 0;
        // config.MaxRadius = 100;
        config.ParticleLifespan = .8f;
        // config.Speed = 100;
        // config.Gravity = new Vector2(10, -10);
        // config.RotationStart = 0;
        // config.RotationEnd = 180;
        // config.MinRadius = 1000;
        config.Speed = 30;
        // config.TangentialAcceleration = 100;
        config.AngleVariance = 180;
        config.Angle = 180;
        // config.RotatePerSecond = 2;
        // config.Gravity = new Vector2(0, 1000);
        return config;
    }

    internal static ParticleEmitterConfig MakeImpatchParticlesConfig()
    {
        var config = new ParticleEmitterConfig();
        config.Sprite = Game.Atlas.GetSprite("circle_particle");

        var alpha = 100;
        var color = Color.FromNonPremultiplied(alpha, alpha, alpha, alpha);
        config.StartColor = color;
        config.StartColorVariance = Color.FromNonPremultiplied(0, 0, 0, 0);
        config.FinishColor = color;
        config.FinishColorVariance = Color.FromNonPremultiplied(0, 0, 0, 0);

        config.MaxParticles = 100;
        // config.EmissionRate = 20;
        config.EmitterType = ParticleEmitterType.Gravity;
        config.Duration = -1;
        config.StartParticleSize = 30;
        config.StartParticleSizeVariance = 10;
        config.FinishParticleSize = 10;
        // config.BlendFuncSource = Microsoft.Xna.Framework.Graphics.Blend.SourceColor;
        // config.BlendFuncDestination = Microsoft.Xna.Framework.Graphics.Blend.DestinationAlpha;
        // config.MinRadius = 0;
        // config.MaxRadius = 100;
        config.ParticleLifespan = 1.2f;
        // config.Speed = 100;
        // config.Gravity = new Vector2(10, -10);
        // config.RotationStart = 0;
        // config.RotationEnd = 180;
        // config.MinRadius = 1000;
        config.Speed = 400;
        config.SpeedVariance = 100;
        // config.TangentialAcceleration = 100;
        config.AngleVariance = 180;
        config.Angle = 0;
        // config.RotatePerSecond = 2;
        config.Gravity = new Vector2(0, 1000);
        return config;
    }
}