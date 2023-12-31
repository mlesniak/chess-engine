namespace chess.Board.Piece;

public class King : Piece
{
    public King(Color color) : base('K', color)
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
                var piece = board.Pieces[ny][nx];
                if (piece != null && Color == piece.Color)
                {
                    continue;
                }

                // BLOCK Remove this.
                if (piece is Block)
                {
                    continue;
                }

                // If this is an opponent, we are allowed to go
                // there, but not further.
                moves.Add(new Move(currentPiece, new Position(nx, ny)));
            }
        }

        return moves;
    }
}
