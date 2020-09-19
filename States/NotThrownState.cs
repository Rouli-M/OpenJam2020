using Nez.AI.FSM;

public class NotThrownState : State<Player>
{
    float minRotation = -80;
    float maxRotation = 0;
    float rotationSpeed = 2;
    bool clockwise = false;

    public override void Begin()
    {
        base.Begin();
        Game.State = GameState.Waiting;
        _context.animator.Play("canon");
    }

    public override void Update(float deltaTime)
    {
        if (_context.IsThrowInputGiven())
            _context.fsm.ChangeState<ThrowingState>();

        if (clockwise)
        {
            if (_context.Entity.RotationDegrees < maxRotation)
                _context.Entity.Rotation += rotationSpeed * deltaTime;
            else
                clockwise = false;
        }
        else
        {
            if (_context.Entity.RotationDegrees > minRotation)
                _context.Entity.Rotation -= rotationSpeed * deltaTime;
            else
                clockwise = true;
        }
    }
}