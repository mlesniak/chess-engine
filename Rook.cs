public class Rook : Piece
{
    public override IEnumerable<Move> ValidMoves(Game game)
    {
        return Enumerable.Empty<Move>();
    }

    public override char Display() => 'r';
    
    public override Rook Copy() => new() { X = X, Y = Y, Color = Color };
}


