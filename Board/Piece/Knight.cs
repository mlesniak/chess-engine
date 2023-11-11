namespace chess.Board.Piece;

public class Knight : Piece
{
    public Knight(Color color) : base(color)
    { }

    public override IEnumerable<Move> AvailableMoves(Board board, Position currentPiece)
    {
        List<Move> moves = new();

        int[] dx = { -2, -1, 1, 2, 2, 1, -1, -2 }; 
        int[] dy = { 1, 2, 2, 1, -1, -2, -2, -1 };

        for (int i = 0; i < 8; i++)
        {
            int nx = currentPiece.X + dx[i];
            int ny = currentPiece.Y + dy[i];
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

            // TODO(mlesniak) remove this later.
            if (piece is Block)
            {
                continue;
            }

            // If this is an opponent, we are allowed to go
            // there, but not further.
            moves.Add(new Move(currentPiece, new Position(nx, ny)));
        }

        return moves;
    }

    // TODO(mlesniak) Can this be done via parent constructor?
    public override char DisplayCharacter() => 'N';
}
