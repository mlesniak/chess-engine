namespace chess.Board.Piece;

/// <summary>
/// A special piece used to block a square from being moved to.
/// This allows us to create special scenarios for testing which
/// limit the possibilities of the board and nudge piece into
/// specific positions.
/// </summary>
public class Block : Piece
{
    public Block(Color color) : base('X', color)
    { }

    // Currently, our rook is just blocking, but not able to
    // do anything for testing purposes.
    public override IEnumerable<Move> AvailableMoves(Board board, Position currentPiece) => Enumerable.Empty<Move>();
}
