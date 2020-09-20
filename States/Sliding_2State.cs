using Nez.AI.FSM;

public class Sliding_2State : State<Player>
{
    public override void Begin()
    {
        base.Begin();
        _context.animator.Play("2-slide");
        _context.flyingParticles.PauseEmission();
    }

    public override void Update(float deltaTime)
    {
        _context.PhysicalUpdate();

        if (_context.IsThrowInputGiven())
            _context.fsm.ChangeState<Throwing_2State>();
    }
}
