namespace chess.Board.Piece;
public class Queen : Piece
{
    public Queen(Color color) : base(color)
    { }

    public override IEnumerable<Move> AvailableMoves(Board board, Position currentPiece)
    {
        List<Move> moves = new();
        // Ignore existing pieces in the first step.
        // Queen can go in any direction. 
        for (var dx = -1; dx <= 1; dx++)
        {
            for (var dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0)
                {
                    continue;
                }

                // Start going into a direction until we reach
                // a border or our piece are able to take an
                // enemy piece.
                var nx = currentPiece.X;
                var ny = currentPiece.Y;
                while (true)
                {
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

                    // TODO(mlesniak) fix this compilation error since we have removed the empty color.

                    // If this is our own color, abort.
                    var piece = board.Pieces[ny][nx];
                    if (piece != null && Color == piece.Color)
                    {
                        break;
                    }

                    moves.Add(new Move(currentPiece, new Position(nx, ny)));

                    // If this is an opponent, we are allowed to go
                    // there, but not further.
                    if (piece != null && Color != piece.Color)
                    {
                        break;
                    }
                }
            }
        }

        return moves;
    }

    public override char DisplayCharacter() => 'Q';
}
