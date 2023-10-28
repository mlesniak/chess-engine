public class Empty : Piece
{
    public override IEnumerable<Move> ValidMoves(Game game) => Enumerable.Empty<Move>();
    public override char Display() => '.';
    public override Empty Copy() => new() { Color = PieceColor.Empty };
}
