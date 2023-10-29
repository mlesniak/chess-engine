public class Rook : Piece
{
    public Rook(Color color) : base(color)
    { }

    public override IEnumerable<Move> ValidMoves(Game game, Color turn, Position currentPiece)
    {
        return Enumerable.Empty<Move>();
    }

    public override char DisplayCharacter() => 'r';

    public override Rook Copy() => new(Color);
} 
