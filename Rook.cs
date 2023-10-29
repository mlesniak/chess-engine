public class Rook : Piece
{
    public Rook(Color color) : base(color)
    { }

    // Currently, our rook is just blocking, but not able to
    // do anything for testing purposes.
    public override IEnumerable<Move> ValidMoves(Game game, Color turn, Position currentPiece)
    {
        return Enumerable.Empty<Move>();
    }

    public override char DisplayCharacter() => 'r';

    public override Rook Copy() => new(Color);
} 
