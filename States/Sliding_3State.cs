using Nez.AI.FSM;

public class Sliding_3State : State<Player>
{
    public override void Begin()
    {
        base.Begin();
        _context.animator.Play("3-slide");
        _context.flyingParticles.PauseEmission();
    }

    public override void Update(float deltaTime)
    {
        _context.PhysicalUpdate();

        if (_context.IsThrowInputGiven())
            _context.fsm.ChangeState<Throwing_3State>();
    }
}
