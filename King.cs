public class King : Piece
{
    public King(Color color) : base(color)
    { }

    public override IEnumerable<Move> ValidMoves(Game game, Color turn, Position currentPiece)
    {
        List<Move> moves = new();
        // Ignore existing pieces in the first step.
        // Queen can go in any direction. 
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0)
                {
                    continue;
                }

                // For this combination, start going into a direction
                // until we reach a border (or alter, our piece or an
                // enemy piece.
                var nx = currentPiece.X;
                var ny = currentPiece.Y;
                nx += dx;
                ny += dy;
                if (nx < 0 || nx > 7)
                {
                    break;
                }
                if (ny < 0 || ny > 7)
                {
                    break;
                }

                // If this is our own color, abort.
                if (Color == game.Board[ny][nx].Color)
                {
                    break;
                }

                // If this is an opponent, we are allowed to go
                // there, but not further.
                moves.Add(new Move(currentPiece, new Position(nx, ny)));
            }
        }

        return moves;
    }

    public override char DisplayCharacter() => 'K';

    // TODO(mlesniak) can we omit this in the future?
    public override King Copy() => new(Color);
}