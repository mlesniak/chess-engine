namespace chess.Board.Piece;

public class King : Piece
{
    public King(Color color) : base(color)
    { }

    public override IEnumerable<Move> AvailableMoves(Board board, Position currentPiece)
    {
        List<Move> moves = new();
        for (var dx = -1; dx <= 1; dx++)
        {
            for (var dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0)
                {
                    continue;
                }

                var nx = currentPiece.X;
                var ny = currentPiece.Y;
                nx += dx;
                ny += dy;
                if (nx < 0 || nx > 7)
                {
                    continue;
                }
                if (ny < 0 || ny > 7)
                {
                    continue;
                }

                // If this is our own color, abort.
                if (Color == board.Pieces[ny][nx].Color)
                {
                    continue;
                }

                // TODO(mlesniak) remove this later.
                if (board.Pieces[ny][nx].GetType() == typeof(Block))
                {
                    continue;
                }

                // If this is an opponent, we are allowed to go
                // there, but not further.
                moves.Add(new Move(currentPiece, new Position(nx, ny)));
            }
        }

        // Prevent moving into chess, which is not a valid move.
        // var ops = board.LegalMoves(turn.Next()).Select(m => m.Dest).ToList();
        // moves = moves.Where(m => !ops.Contains(m.Dest)).ToList();

        return moves;
    }

    // TODO(mlesniak) can this be a constructor parameter?
    public override char DisplayCharacter() => 'K';

    // TODO(mlesniak) can we omit this in the future?
    public override King Copy() => new(Color);
}
