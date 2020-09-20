using Nez.AI.FSM;

public class Sliding_1State : State<Player>
{
    public override void Begin()
    {
        base.Begin();
        _context.animator.Play("1-slide");
        _context.flyingParticles.PauseEmission();
    }

    public override void Update(float deltaTime)
    {
        _context.PhysicalUpdate();
    }
}
