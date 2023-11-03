public class Empty : Piece
{
    public Empty() : base(Color.Empty)
    { }

    public override IEnumerable<Move> ValidMoves(Game game, Color turn, Position currentPiece) => Enumerable.Empty<Move>();
    public override char DisplayCharacter() => '.';
    public override Empty Copy() => new() { Color = Color.Empty };
}
