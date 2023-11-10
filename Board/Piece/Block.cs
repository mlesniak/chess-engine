namespace chess.Board.Piece;

public class Block : Piece
{
    public Block(Color color) : base(color)
    { }

    // Currently, our rook is just blocking, but not able to
    // do anything for testing purposes.
    public override IEnumerable<Move> AvailableMoves(Board board, Color turn, Position currentPiece) => Enumerable.Empty<Move>();

    public override char DisplayCharacter() => 'X';

    public override Block Copy() => new(Color);
}
