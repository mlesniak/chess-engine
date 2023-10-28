public class Rook : Piece
{
    public override IEnumerable<Move> ValidMoves(Game game)
    {
        return Enumerable.Empty<Move>();
    }

    public override char Display() => 'r';
}


