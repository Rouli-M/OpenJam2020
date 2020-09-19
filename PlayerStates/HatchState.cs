using Nez.AI.FSM;

public class HatchState : State<Player>
{
    public override void Begin()
    {
        base.Begin();
        Game.State = GameState.Over;

        _context.animator.Play("1-win");
        _context.success.Play();
    }

    public override void Update(float deltaTime) { }
}
