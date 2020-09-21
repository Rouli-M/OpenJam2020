using Microsoft.Xna.Framework.Media;
using Nez.AI.FSM;
using OpenJam2020.Components;

public class ThrowingState : State<Player>
{
    public override void Begin()
    {
        base.Begin();
        Game.State = GameState.Playing;

        _context.animator.Play("canon_loaded");
        _context.charge_canon_sound.Play();
    }

    public override void Update(float deltaTime)
    {
        if (!_context.IsThrowInputGiven())
        {
            var entity = _context.Entity.Scene.CreateEntity("canon_empty", _context.Transform.Position).AddComponent(new DroppedDino(0));
            entity.Transform.Rotation = _context.Entity.Rotation;
            MediaPlayer.Play(_context.music);
            MediaPlayer.Volume = 0.6f;
            MediaPlayer.IsRepeating = true;

            var direction = _context.Throw(1200f, -_context.Entity.Rotation);
            _context.Transform.Position += direction * 600;
            _context.fsm.ChangeState<Flying_3State>();
        }
    }
}
